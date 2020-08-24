using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.IO;

namespace DataAgent
{
    class SettingIO
    {
        #region 配置文件声明变量
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        private static string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "FileConfig.ini";//获取INI文件路径
        private string strSec = ""; //INI文件名
        #endregion

        #region 配置文件读取
        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        static public string ContentValue(string Section, string key)
        {
            if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
                return temp.ToString();
            }
            else
            {
                return "error";
            }
        }
        #endregion

        #region 配置文件写入
        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        static public void ContentWrite(string Section, string key, string value)
        {
            if (File.Exists(strFilePath))//读取时先要判读INI文件是否存在
            {
                WritePrivateProfileString(Section, key, value, strFilePath);
            }
            else
            {
            }
            
        }
        #endregion


    }
}
