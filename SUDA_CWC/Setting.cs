using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SUDA_CWC
{
    public partial class Setting : Form
    {
        Form1 form;
        public Setting(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            textBox1.Text = Properties.Settings.Default.username;
            textBox2.Text = Properties.Settings.Default.password;
            textBox3.Text = Properties.Settings.Default.project_id;
            textBox4.Text = Properties.Settings.Default.charge_name;
            textBox5.Text = Properties.Settings.Default.attach_num;
            textBox6.Text = Properties.Settings.Default.default_abstract;
            textBox7.Text = Properties.Settings.Default.card_id;
            textBox8.Text = Properties.Settings.Default.card_name;
            textBox9.Text = Properties.Settings.Default.card_num;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.username = textBox1.Text;
            Properties.Settings.Default.password = textBox2.Text;
            Properties.Settings.Default.project_id = textBox3.Text;
            Properties.Settings.Default.charge_name = textBox4.Text;
            Properties.Settings.Default.attach_num = textBox5.Text;
            Properties.Settings.Default.default_abstract = textBox6.Text;
            Properties.Settings.Default.card_id = textBox7.Text;
            Properties.Settings.Default.card_name = textBox8.Text;
            Properties.Settings.Default.card_num = textBox9.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("已保存!");

            form.LoadSettings();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            /*foreach(Control c in this.Controls)
            {
                if("System.Windows.Forms.TextBox" == c.GetType().ToString()){
                    ((TextBox)c).Text = "";
                }
            }*/
            textBox1.Text = Properties.Settings.Default.username;
            textBox2.Text = Properties.Settings.Default.password;
            textBox3.Text = Properties.Settings.Default.project_id;
            textBox4.Text = Properties.Settings.Default.charge_name;
            textBox5.Text = Properties.Settings.Default.attach_num;
            textBox6.Text = Properties.Settings.Default.default_abstract;
            textBox7.Text = Properties.Settings.Default.card_id;
            textBox8.Text = Properties.Settings.Default.card_name;
            textBox9.Text = Properties.Settings.Default.card_num;
            MessageBox.Show("清理完成!");
        }
    }
}
