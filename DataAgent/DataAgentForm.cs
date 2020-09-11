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

        private string strFilePath = Application.StartupPath + "\\FileConfig.ini";//ini配置文件必须放在当前文件夹下
        private string strFileName = ""; //INI文件名
        int[] defaultR220 = new int[5] { 0, 0, 0, 0, 0 };
        int[] R220 = new int[5];
        private List<string> availableSensorList;
        SyntecRemoteCNC cnc;//cnc系统连接变量

        public DataAgentForm()
        {
            InitializeComponent();
            availableSensorList = new List<string>();
            MainTimer.Enabled = false;
        }

        private void 自动连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            string cncIP;
            strFileName = Path.GetFileNameWithoutExtension(strFilePath);
            cncIP = SettingIO.ContentValue(strFilePath, strFileName, "CNC_IP");
            cnc = new SyntecRemoteCNC(cncIP);
            if (!cnc.isConnected())
            {
                MessageBox.Show("CNC Connection Error!");
            }
            else
            {
                label_mainDevStatus.Text = "已连接";
                label_mainDevStatus.Image = Properties.Resources.green_128;
                label_workingStatus.Text = "未起弧";
                label_workingStatus.Image = Properties.Resources.green_128;
                availableSensorList = SelfCheck();
                if (availableSensorList.Count > 0)
                {
                    MainTimer.Enabled = true;
                }
            }

        }

        private void 手动连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualConnect manualConnect = new ManualConnect();
            manualConnect.ShowDialog();
            自动连接ToolStripMenuItem_Click(sender, e);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            cnc.READ_plc_register(220, 224, out R220);
            if (R220[0] == 1)
            {
                StartAllAvailableSensor(availableSensorList);
                cnc.WRITE_plc_register(220, 224, defaultR220);
            }
            else if (R220[1] == 1)
            {
                StopAllAvailableSensor(availableSensorList);
                cnc.WRITE_plc_register(220, 224, defaultR220);
            }
        }

        /// <summary>
        /// 自检程序，确定可用的传感器
        /// </summary>
        /// <returns>可用传感器列表</returns>
        private List<string> SelfCheck()
        {
            return null;
        }

        /// <summary>
        /// 打开所有可用传感器的采集功能
        /// </summary>
        /// <param name="availableSensorList">可用传感器列表</param>
        private void StartAllAvailableSensor(List<string> availableSensorList)
        {
            label_workingStatus.Text = "起弧中";
            label_workingStatus.Image = Properties.Resources.red_128;
        }

        /// <summary>
        /// 保存数据并停止所有可用传感器的采集功能
        /// </summary>
        /// <param name="availableSensorList">可用传感器列表</param>
        private void StopAllAvailableSensor(List<string> availableSensorList)
        {
            label_workingStatus.Text = "未起弧";
            label_workingStatus.Image = Properties.Resources.green_128;
        }
    }
}
