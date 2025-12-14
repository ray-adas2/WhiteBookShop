using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaoBaiBookstore
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //以模态方式显示 Splash，等待其完成（例如 Splash 自行 Close()）
            using (var splash = new Splash())
            {
                splash.ShowDialog();
            }

            //运行 Login 作为主窗体，关闭 Login 即退出应用
            Application.Run(new Login());
        }
    }
}
