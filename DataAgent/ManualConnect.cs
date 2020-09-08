using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace DataAgent
{
    public partial class ManualConnect : Form
    {
        private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//获取INI文件路径
        private string strFileName = ""; //INI文件名
        bool textBoxValid = true;
        public ManualConnect()
        {
            InitializeComponent();
                string cncIP;
                strFileName = Path.GetFileNameWithoutExtension(strFilePath);
                cncIP = SettingIO.ContentValue(strFilePath, strFileName, "CNC_IP");
                setIpAddress1(cncIP);
        }

        #region 确保IP地址输入合法
        private void textBoxOnTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int len = textBox.Text.Length;
            if (len == 3)
            {
                string currentStr = textBox.Text;
                int currentNum = Convert.ToInt32(currentStr);
                if (currentNum > 255)
                {
                    MessageBox.Show(currentNum + "Is Not Valid. Please Enter A Value Between 1 And223.", "Error");
                    textBoxValid = false;
                    textBox.Text = "255";
                }
                else
                {
                    if (textBoxValid)
                    {
                        if (textBox == textBox4)
                        { return; }
                        else
                        { SendKeys.Send("{Tab}"); }
                    }
                    else
                    {
                        textBox.SelectionStart = 0;
                        textBoxValid = true;
                    }
                }
            }
        }
        private void textBoxOnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)8)
            {
                if (e.KeyChar == (char)8)
                {
                    e.Handled = false;
                    return;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (e.KeyValue == 37 || e.KeyValue == 38 || e.KeyValue == 8)
            {
                if (textbox.SelectionStart == 0)
                {
                    SendKeys.Send("{Tab}");
                }
            }
            else if (e.KeyValue == 39 || e.KeyValue == 40)
            {
                string currentStr = textbox.Text;
                if (textbox.SelectionStart == currentStr.Length)
                {
                    SendKeys.Send("+{Tab}{End}");
                }
            }
        }
        #endregion

        #region IPAddress1 textBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int len = textBox1.Text.Length;
            if (len == 3)
            {
                string currentStr = textBox1.Text;
                int currentNum = Convert.ToInt32(currentStr);
                if (currentNum > 223)
                {
                    MessageBox.Show(currentNum + "Is Not Valid. Please Enter A Value Between 1 And223.", "Error");
                    textBoxValid = false;
                    textBox1.Text = "223";
                }
                else if (currentNum == 0)
                {
                    MessageBox.Show(currentNum + "Is Not Valid. Please Enter A Value Between 1 And223.", "Error");
                    textBoxValid = false;
                    textBox1.Text = "1";
                }
                else
                {
                    if (textBoxValid)
                    { SendKeys.Send("{Tab}"); }
                    else
                    {
                        textBox1.SelectionStart = 0;
                        textBoxValid = true;
                    }
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBoxOnTextChanged(sender, e);
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBoxOnTextChanged(sender, e);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBoxOnTextChanged(sender, e);
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxOnKeyPress(e);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxOnKeyPress(e);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBoxOnKeyPress(e);
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)8)
            {
                if (e.KeyChar == (char)8)
                {
                    e.Handled = false;
                    return;
                }
                if (textBox4.TextLength == 3)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxOnKeyDown(sender, e);
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxOnKeyDown(sender, e);
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxOnKeyDown(sender, e);
        }
        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxOnKeyDown(sender, e);
        }
        public string ipAddress1String()
        {
            string ipAddress = textBox1.Text.Trim() + "." + textBox2.Text.Trim() + "." + textBox3.Text.Trim() + "." + textBox4.Text.Trim();
            return ipAddress;
        }
        public bool ipAddress1NotNull()
        {
            if (textBox1.Text.Trim() != string.Empty && textBox2.Text.Trim() != string.Empty &&
                textBox3.Text.Trim() != string.Empty && textBox4.Text.Trim() != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setIpAddress1(string ipAddressStr)
        {
            string[] textIpAddress = ipAddressStr.Split('.');
            try
            {
                textBox1.Text = textIpAddress[0];
                textBox2.Text = textIpAddress[1];
                textBox3.Text = textIpAddress[2];
                textBox4.Text = textIpAddress[3];
            }
            catch { }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (ipAddress1NotNull())
            {
                SettingIO.ContentWrite(strFilePath, strFileName, "CNC_IP", ipAddress1String());
            }
            Close();
        }
    }
}
