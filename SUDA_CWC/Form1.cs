using Microsoft.Win32;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SUDA_CWC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            SetWebBrowserFeatures(GetBrowserVersion());
            InitializeComponent();
            LoadLib();
            LoadSettings();
        }

        /// <summary>  
        /// 修改注册表信息来兼容当前程序  
        ///   
        /// </summary>  
        private static void SetWebBrowserFeatures(int ieVersion)
        {
            // don't change the registry if running in-proc inside Visual Studio  
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
                return;
            //获取程序及名称  
            var appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            //得到浏览器的模式的值  
            UInt32 ieMode = GeoEmulationModee(ieVersion);
            var featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            //设置浏览器对应用程序（appName）以什么模式（ieMode）运行  
            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION",
                appName, ieMode, RegistryValueKind.DWord);
            // enable the features which are "On" for the full Internet Explorer browser  
            //不晓得设置有什么用  
            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION",
                appName, 1, RegistryValueKind.DWord);
        }

        /// <summary>  
        /// 获取浏览器的版本  
        /// </summary>  
        /// <returns></returns>  
        private static int GetBrowserVersion()
        {
            int browserVersion = 0;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }
            //如果小于7  
            if (browserVersion < 7)
            {
                throw new ApplicationException("不支持的浏览器版本!");
            }
            return browserVersion;
        }

        /// <summary>  
        /// 通过版本得到浏览器模式的值  
        /// </summary>  
        /// <param name="browserVersion"></param>  
        /// <returns></returns>  
        private static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.  
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11  
                    break;
            }
            return mode;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle;
        }

        private void btn_fillone_Click(object sender, EventArgs e)
        {
            if ("http://cwpt.suda.edu.cn/WFManager/login.jsp" != webBrowser1.Url.ToString())
            {
                MessageBox.Show("不正确的页面, 不要乱用功能!");
            }
            else if (username == "" || password == "")
            {
                MessageBox.Show("用户名或密码不完整!");
                return;
            }
            else
            {
                //根据验证码ID获取验证码元素
                HtmlElement ImageCodeTag = webBrowser1.Document.GetElementById("checkcodeImg");
                //获取网页所有内容
                HTMLDocument hdoc = (HTMLDocument)webBrowser1.Document.DomDocument;
                //获取网页body标签中的内容
                HTMLBody hbody = (HTMLBody)hdoc.body;
                //创建一个接口
                IHTMLControlRange hcr = (IHTMLControlRange)hbody.createControlRange();
                //获取图片地址
                IHTMLControlElement hImg = (IHTMLControlElement)ImageCodeTag.DomElement;
                //将图片添加到接口中
                hcr.add(hImg);
                //将图片复制到内存
                hcr.execCommand("Copy", false, null);
                //从粘贴板得到图片
                Image CodeImage = Clipboard.GetImage();
                //返回得到的验证码
                //CodeImage.Save("123.png");

                // 识别验证码
                byte[] Buffer = ImageToBytes(CodeImage);
                StringBuilder Result = new StringBuilder('\0', 256);
                string coderesult = "";
                if (GetImageFromBuffer(Buffer, Buffer.Length, Result))
                    coderesult = Result.ToString();
                else
                    coderesult = "识别失败";

                //填充用户名密码验证码
                HtmlElement name = webBrowser1.Document.GetElementById("uid");
                if (name != null)
                    name.SetAttribute("value", username);
                HtmlElement pass = webBrowser1.Document.GetElementById("pwd");
                if (pass != null)
                    pass.SetAttribute("value", password);
                HtmlElement code = webBrowser1.Document.GetElementById("chkcode");
                if (code != null)
                    code.SetAttribute("value", coderesult);

                HtmlElement loginbtn = webBrowser1.Document.GetElementById("loginbtn2");
                if (loginbtn != null)
                    loginbtn.InvokeMember("click");
            }
        }
        // 识别读取验证码byte序列
        public byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                /*if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    MessageBox.Show("1");
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                    MessageBox.Show("2");
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                    MessageBox.Show("3");
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                    MessageBox.Show("4");
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                    MessageBox.Show("5");
                }*/
                image.Save(ms, ImageFormat.Png);
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        [DllImport("WmCode.dll")]
        public static extern bool LoadWmFromFile(string FilePath, string Password);

        [DllImport("WmCode.dll")]
        public static extern bool GetImageFromBuffer(byte[] FileBuffer, int ImgBufLen, StringBuilder Vcode);

        [DllImport("WmCode.dll")]
        public static extern bool SetWmOption(int OptionIndex, int OptionValue);

        // 加载验证码识别库
        private void LoadLib()
        {
            if (LoadWmFromFile("SUDA_CWC.dat", "123456"))
            {
                SetWmOption(6, 90);
            }
        }

        private void btn_filltwo_Click(object sender, EventArgs e)
        {
            IHTMLDocument2 document2 = null;
            for (int i = 0; i < webBrowser1.Document.Window.Frames.Count; i++)
            {
                IHTMLDocument2 temp = (IHTMLDocument2)webBrowser1.Document.Window.Frames[i].Document.DomDocument;
                if("about:blank" != temp.url.ToString())
                {
                    document2 = temp;
                    break;
                }
            }
            if(document2 == null)
            {
                MessageBox.Show("填充失败, 是否当前不在这个页面?");
            }
            else
            {
                IHTMLDocument3 document3 = (IHTMLDocument3)document2;
                IHTMLElement prj_code = document3.getElementById("formWF_YB_NEW_230_yta-uni_prj_code");
                if(prj_code != null)
                    prj_code.setAttribute("value", project_id);
                IHTMLElement charge_name2 = document3.getElementById("formWF_YB_NEW_230_up-charge_name");
                if (charge_name2 != null)
                    charge_name2.setAttribute("value", charge_name);
                IHTMLElement addition = document3.getElementById("formWF_YB_NEW_230_yta-addition");
                if (addition != null)
                    addition.setAttribute("value", attach_num);
                IHTMLElement remark = document3.getElementById("formWF_YB_NEW_230_yta-remark");
                if (remark != null)
                    remark.setAttribute("value", default_abstract); 
            }
        }

        private void btn_fillthree_Click(object sender, EventArgs e)
        {
            IHTMLDocument2 document2 = null;
            for (int i = 0; i < webBrowser1.Document.Window.Frames.Count; i++)
            {
                IHTMLDocument2 temp = (IHTMLDocument2)webBrowser1.Document.Window.Frames[i].Document.DomDocument;
                if ("about:blank" != temp.url.ToString())
                {
                    document2 = temp;
                    break;
                }
            }
            if (document2 == null)
            {
                MessageBox.Show("填充失败, 是否当前不在这个页面?");
            }
            else
            {
                IHTMLDocument3 document3 = (IHTMLDocument3)document2;
                IHTMLElement card_sno = document3.getElementById("formWF_YB_NEW_4270_p-card_sno1_0");
                if (card_sno != null)
                    card_sno.setAttribute("value", card_id);
                IHTMLElement card_name2 = document3.getElementById("formWF_YB_NEW_4270_p-card_name1_0");
                if (card_name2 != null)
                    card_name2.setAttribute("value", card_name);
                IHTMLElement card_remark = document3.getElementById("formWF_YB_NEW_4270_p-card_remark1_0");
                if (card_remark != null)
                    card_remark.setAttribute("value", card_num);
                string WTAMTstring = "";
                IHTMLElement WTAMT = document3.getElementById("formWF_YB_NEW_4272_p-WTAMT");
                if (WTAMT != null)
                    WTAMTstring = WTAMT.getAttribute("value").ToString();             
                IHTMLElement card_amt = document3.getElementById("formWF_YB_NEW_4270_p-card_amt1_0");
                if (card_amt != null)
                    card_amt.setAttribute("value", WTAMTstring);
            }
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(this);
            setting.ShowDialog();
        }

        string username = "";
        string password = "";
        string project_id = "";
        string charge_name = "";
        string attach_num = "";
        string default_abstract = "";
        string card_id = "";
        string card_name = "";
        string card_num = "";
        public void LoadSettings()
        {
            username = Properties.Settings.Default.username;
            password = Properties.Settings.Default.password;
            project_id = Properties.Settings.Default.project_id;
            charge_name = Properties.Settings.Default.charge_name;
            attach_num = Properties.Settings.Default.attach_num;
            default_abstract = Properties.Settings.Default.default_abstract;
            card_id = Properties.Settings.Default.card_id;
            card_name = Properties.Settings.Default.card_name;
            card_num = Properties.Settings.Default.card_num;
        }
    }
}
