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

using System.Runtime.InteropServices;
using Syntec.Remote;

namespace DataAgent
{
    public partial class DataAgentForm : Form
    {
        //#region 配置文件声明变量
        ///// <summary>
        ///// 写入INI文件
        ///// </summary>
        ///// <param name="section">节点名称[如[TypeName]]</param>
        ///// <param name="key">键</param>
        ///// <param name="val">值</param>
        ///// <param name="filepath">文件路径</param>
        ///// <returns></returns>
        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        ///// <summary>
        ///// 读取INI文件
        ///// </summary>
        ///// <param name="section">节点名称</param>
        ///// <param name="key">键</param>
        ///// <param name="def">值</param>
        ///// <param name="retval">stringbulider对象</param>
        ///// <param name="size">字节大小</param>
        ///// <param name="filePath">文件路径</param>
        ///// <returns></returns>
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        //private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//获取INI文件路径
        //private string strSec = ""; //INI文件名
        //#endregion

        //#region 配置文件读取
        ///// <summary>
        ///// 自定义读取INI文件中的内容方法
        ///// </summary>
        ///// <param name="Section">键</param>
        ///// <param name="key">值</param>
        ///// <returns></returns>
        //private string ContentValue(string Section, string key)
        //{

        //    StringBuilder temp = new StringBuilder(1024);
        //    GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
        //    return temp.ToString();
        //}
        //#endregion

        private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名
        SyntecRemoteCNC m_cnc;

        public DataAgentForm()
        {
            InitializeComponent();
        }




        private void 自动连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            string cncIP;
            strSec = Path.GetFileNameWithoutExtension(strFilePath);
            cncIP = SettingIO.ContentValue(strSec, "CNC_IP");
            m_cnc = new SyntecRemoteCNC(cncIP);
            if (!m_cnc.isConnected())
            {
                MessageBox.Show("CNC Connection Error!");
            }

        }

        private void 手动连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualConnect manuakConnect = new ManualConnect();
            manuakConnect.ShowDialog();
            自动连接ToolStripMenuItem_Click(sender, e);
        }
    }
}
