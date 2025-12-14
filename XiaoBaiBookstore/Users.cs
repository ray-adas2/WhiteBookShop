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
    public partial class Users : Form
    {
        private FormMove _formMove;
        public Users()
        {
            InitializeComponent();

            _formMove = new FormMove(this);
            DataShow();
        }

        static string DBKey = "Data Source=RAY;Initial Catalog=WhiteBookShopDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";//数据库密钥

        SqlConnection myDB = new SqlConnection(DBKey);//创建数据库连接对象

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PhoneTb.Text == "" || AddressTb.Text == "" || PasswordTb.Text == "")//检查是否有空字段
            {
                MessageBox.Show("缺少信息，请填写完整的信息");
                return;
            }
            else
            {
                try
                {
                    myDB.Open();

                    string query = $"insert into UserTb1 (UName,UPhone,UAddress,UPassword) values('{UNameTb.Text}','{PhoneTb.Text}','{AddressTb.Text}','{PasswordTb.Text}')";
                    SqlCommand cmd = new SqlCommand(query, myDB);//创建SQL命令对象
                    cmd.ExecuteNonQuery();//执行SQL命令
                    MessageBox.Show("用户信息保存成功");
                    myDB.Close();
                    DataShow();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    myDB.Close();
                }
            }
        }

        private void DataShow()//显示数据库中的书籍信息
        {
            myDB.Open();
            string query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(query, myDB);//创建数据适配器对象
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);//创建命令生成器对象
            var ds = new DataSet();//创建数据集对象
            sda.Fill(ds);//填充数据集
            UserDGV.DataSource = ds.Tables[0];//将数据集绑定到DataGridView控件
            myDB.Close();
        }

        private void Reset()//重置输入字段
        {
            UNameTb.Text = "";
            PhoneTb.Text = "";
            AddressTb.Text = "";
            PasswordTb.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出应用程序
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        int key = 0;//用于存储选中行的主键值
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("相关内容为空，无法删除！");
                return;
            }
            else
            {
                try
                {
                    string query = "delete from UserTB1 where UId = @id";
                    using (var cmd = new SqlCommand(query, myDB))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = key;
                        myDB.Open();
                        int affected = cmd.ExecuteNonQuery();
                        myDB.Close();

                        if (affected > 0) MessageBox.Show("删除成功！");
                        else MessageBox.Show("未找到要删除的记录。");
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
        }

        private void UserDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //忽略表头或无效点击
            if (e.RowIndex < 0)
                return;

            UNameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            PhoneTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            AddressTb.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            PasswordTb.Text = UserDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (UNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());//获取选中行的主键值
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PhoneTb.Text == "" || AddressTb.Text == "" || PasswordTb.Text == "" )//检查是否有空字段
            {
                MessageBox.Show("缺少信息，无法对数据进行修改");
                return;
            }
            else
            {
                try
                {
                    string query = $"update UserTB1 set UName = '{UNameTb.Text}',UPhone = '{PhoneTb.Text}',UAddress = '{AddressTb.Text}',UPassword = '{PasswordTb.Text}' where UId ={key}";
                    using (var cmd = new SqlCommand(query, myDB))
                    {
                        myDB.Open();
                        int affected = cmd.ExecuteNonQuery();
                        myDB.Close();

                        if (affected > 0) MessageBox.Show("修改成功！");
                        else MessageBox.Show("未找到要被修改的记录。");
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
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books login = new Books();
            login.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AccountManagement login = new AccountManagement();
            login.Show();
            this.Hide();
        }
    }
}
