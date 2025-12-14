using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiaoBaiBookstore
{
    public partial class Billing : Form
    {
        private FormMove _formMove;
        public Billing()
        {
            InitializeComponent();

            DataShow();
            _formMove = new FormMove(this);
        }

        static string DBKey = "Data Source=RAY;Initial Catalog=WhiteBookShopDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";//数据库密钥

        SqlConnection myDB = new SqlConnection(DBKey);//创建数据库连接对象

        int key = 0;//用于存储选中行的主键值
        int stock = 0;//用于存储选中书籍的库存数量
        decimal GrdTotal = 0;//用户消费的钱数
        private void DataShow()//显示数据库中的书籍信息
        {
            myDB.Open();
            string query = "select * from BookTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, myDB);//创建数据适配器对象
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);//创建命令生成器对象
            var ds = new DataSet();//创建数据集对象
            sda.Fill(ds);//填充数据集
            BookDGV.DataSource = ds.Tables[0];//将数据集绑定到DataGridView控件
            myDB.Close();
        }

        int num = 1;//用于计数账单项
        private void AddtoBillBtn_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")//
            {
                MessageBox.Show("没有添加购买数量");
            }
            else if(Convert.ToInt32(QtyTb.Text) > stock)
            {
                MessageBox.Show("库存不足！");
            }
            else
            {
                decimal total = Convert.ToInt32(QtyTb.Text) * Convert.ToDecimal(BPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = num;
                newRow.Cells[1].Value = BTitleTb.Text;
                newRow.Cells[2].Value = BPriceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                num++;
                UpdateBook();
                GrdTotal = GrdTotal + total;
                TotalLbl.Text = "订单总额:" + GrdTotal + "元";
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Filter()//根据类别筛选书籍
        {
            myDB.Open();
            string query = $"select * from BookTb1 where BCat ='{CatCbSearch.SelectedItem.ToString()}'";
            SqlDataAdapter sda = new SqlDataAdapter(query, myDB);//创建数据适配器对象
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);//创建命令生成器对象
            var ds = new DataSet();//创建数据集对象
            sda.Fill(ds);//填充数据集
            BookDGV.DataSource = ds.Tables[0];//将数据集绑定到DataGridView控件
            myDB.Close();
        }

        private void CatCbSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void BookDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //忽略表头或无效点击
            if (e.RowIndex < 0)
                return;

            BTitleTb.Text = BookDGV.SelectedRows[0].Cells[1].Value.ToString();
            BPriceTb.Text = BookDGV.SelectedRows[0].Cells[5].Value.ToString();
            //QtyTb.Text = BookDGV.SelectedRows[0].Cells[4].Value.ToString();
            

            if (BTitleTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[0].Value.ToString());//获取选中行的主键值
                stock = Convert.ToInt32(BookDGV.SelectedRows[0].Cells[4].Value.ToString());//获取选中书籍的库存数量
            }
        }

        private void Reset()
        {
            BTitleTb.Text = "";
            BPriceTb.Text = "";
            QtyTb.Text = "";
        }

        private void UpdateBook()//只用于更新库存数量,加入购物车,书店库存就减少
        {
            int newQty = stock - Convert.ToInt32(QtyTb.Text);//新库存数
            try
            {
                string query = $"update BookTB1 set BQty = '{newQty}' where BId ={key}";
                using (var cmd = new SqlCommand(query, myDB))
                {
                    myDB.Open();
                    myDB.Close();
                }

                key = 0;
                DataShow();
                Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (myDB.State == ConnectionState.Open) myDB.Close();
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            UserNameLbl.Text = Login.UserName;
        }

        int prodid, prodqty, tottal, pos = 60;
        decimal prodprice;

        private void Update_Click(object sender, EventArgs e)
        {
            DataShow();
            CatCbSearch.SelectedIndex = -1;//重置筛选类别
        }

        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("小白书店", new Font("幼圆", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("编号   产品   价格   数量   总计", new Font("幼圆",10,FontStyle.Bold),Brushes.Red,new Point(26,40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column7"].Value);
                prodname = "" + row.Cells["Column8"].Value;
                prodprice = Convert.ToDecimal(row.Cells["Column9"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column10"].Value);
                tottal = Convert.ToInt32(row.Cells["Column11"].Value);
                e.Graphics.DrawString("" + prodid, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(30, pos));
                e.Graphics.DrawString("" + prodname, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(62, pos));
                e.Graphics.DrawString("" + prodprice, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(130, pos));
                e.Graphics.DrawString("" + prodqty, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(180, pos));
                e.Graphics.DrawString("" + tottal, new Font("幼圆", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
           
            }
            e.Graphics.DrawString("订单总额：￥" + GrdTotal, new Font("幼圆", 12, FontStyle.Bold), Brushes.Crimson, new Point(60, pos+50));
            e.Graphics.DrawString("--------------小白书店--------------", new Font("幼圆", 10, FontStyle.Bold), Brushes.Crimson, new Point(15, pos+85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {

            if (BillDGV.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("购物车里暂无书籍哦");
                return;
            }
            else
            {
                try
                {
                    myDB.Open();

                    string query = $"insert into BillTb1 (UName,Amount) values('{UserNameLbl.Text}','{GrdTotal}')";
                    SqlCommand cmd = new SqlCommand(query, myDB);//创建SQL命令对象
                    cmd.ExecuteNonQuery();//执行SQL命令
                    MessageBox.Show("订单信息保存成功");

                    myDB.Close();
                    UpdateBook();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    myDB.Close();
                }
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();

                }
            }
        }
    }
}
