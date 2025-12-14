using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaoBaiBookstore
{
    public partial class Splash : Form
    {
        private FormMove _formMove;//窗体移动功能对象
        public Splash()
        {
            InitializeComponent();
            _formMove = new FormMove(this);//初始化窗体移动功能
        }

        int startpoint = 0;//进度条初始值
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;//每次增加1
            timer1.Interval = 50;//设置计时器间隔时间为50毫秒
            Myprogress.Value = startpoint;//设置进度条的值
            PercentageLbl.Text = startpoint + "%";//显示百分比

            if (Myprogress.Value >= 100)
            {
                Myprogress.Value = 0;//重置进度条
                startpoint = 0;//重置进度条初始值
                timer1.Stop();//停止计时器

                /*Login Log = new Login();//实例化登录窗体，打开登录窗口
                Log.Show();//显示登录窗体*/
                this.Close();//关闭当前窗体(加载窗体)
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();//启动计时器
        }
    }
}
