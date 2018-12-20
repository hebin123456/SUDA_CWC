namespace SUDA_CWC
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_fillthree = new System.Windows.Forms.Button();
            this.btn_filltwo = new System.Windows.Forms.Button();
            this.btn_fillone = new System.Windows.Forms.Button();
            this.btn_setting = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(980, 570);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://cwpt.suda.edu.cn/WFManager/login.jsp", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_fillthree);
            this.splitContainer1.Panel1.Controls.Add(this.btn_filltwo);
            this.splitContainer1.Panel1.Controls.Add(this.btn_fillone);
            this.splitContainer1.Panel1.Controls.Add(this.btn_setting);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(980, 604);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_fillthree
            // 
            this.btn_fillthree.Location = new System.Drawing.Point(893, 7);
            this.btn_fillthree.Name = "btn_fillthree";
            this.btn_fillthree.Size = new System.Drawing.Size(75, 23);
            this.btn_fillthree.TabIndex = 3;
            this.btn_fillthree.Text = "填充卡号";
            this.btn_fillthree.UseVisualStyleBackColor = true;
            this.btn_fillthree.Click += new System.EventHandler(this.btn_fillthree_Click);
            // 
            // btn_filltwo
            // 
            this.btn_filltwo.Location = new System.Drawing.Point(587, 7);
            this.btn_filltwo.Name = "btn_filltwo";
            this.btn_filltwo.Size = new System.Drawing.Size(75, 23);
            this.btn_filltwo.TabIndex = 2;
            this.btn_filltwo.Text = "填充项目";
            this.btn_filltwo.UseVisualStyleBackColor = true;
            this.btn_filltwo.Click += new System.EventHandler(this.btn_filltwo_Click);
            // 
            // btn_fillone
            // 
            this.btn_fillone.Location = new System.Drawing.Point(265, 7);
            this.btn_fillone.Name = "btn_fillone";
            this.btn_fillone.Size = new System.Drawing.Size(75, 23);
            this.btn_fillone.TabIndex = 1;
            this.btn_fillone.Text = "填充密码";
            this.btn_fillone.UseVisualStyleBackColor = true;
            this.btn_fillone.Click += new System.EventHandler(this.btn_fillone_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.Location = new System.Drawing.Point(12, 7);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(75, 23);
            this.btn_setting.TabIndex = 0;
            this.btn_setting.Text = "设置";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 604);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button btn_fillthree;
        private System.Windows.Forms.Button btn_filltwo;
        private System.Windows.Forms.Button btn_fillone;
    }
}

