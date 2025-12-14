using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaoBaiBookstore
{
    public class FormMove
    {
        private Form _form;
        private Point _mouseDownLocation;

        public FormMove(Form form)
        {
            _form = form;
            Initialize();
        }

        private void Initialize()
        {
            //绑定窗体的鼠标事件
            _form.MouseDown += Form_MouseDown;
            _form.MouseMove += Form_MouseMove;
            _form.MouseUp += Form_MouseUp;
        }

        //鼠标按下事件
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //记录鼠标按下的位置
                _mouseDownLocation = e.Location;
            }
        }

        //鼠标移动事件
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //计算鼠标的偏移量并移动窗体
                _form.Location = new Point(
                    _form.Location.X + (e.X - _mouseDownLocation.X),
                    _form.Location.Y + (e.Y - _mouseDownLocation.Y)
                );
            }
        }

        //鼠标松开事件
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            //清空记录按下位置
            if (e.Button == MouseButtons.Left)
                _mouseDownLocation = Point.Empty;
        }
    }
}
