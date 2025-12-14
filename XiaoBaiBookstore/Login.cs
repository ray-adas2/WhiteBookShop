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
    public partial class Login : Form
    {
        private FormMove _formMove;//窗体移动功能对象
        public Login()
        {
            InitializeComponent();
            _formMove = new FormMove(this);//初始化窗体移动功能
        }

        static string DBKey = "Data Source=RAY;Initial Catalog=WhiteBookShopDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";//数据库密钥

        SqlConnection myDB = new SqlConnection(DBKey);//创建数据库连接对象

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出应用程序
        }

        public static string UserName = "";
        public static int UID = 0;//暂时未使用
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //使用参数化查询防止SQL注入
                string query = "select count(*) from UserTb1 where UName = @UName and UPassword = @UPassword";

                using (SqlCommand cmd = new SqlCommand(query, myDB))
                {
                    //根据字段类型使用正确的SqlDbType
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = UNameTb.Text.Trim();
                    cmd.Parameters.Add("@UPassword", SqlDbType.VarChar).Value = UPasswordTb.Text;

                    myDB.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        UserName = UNameTb.Text;
                        Billing bill = new Billing();
                        bill.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"数据库错误: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"登录失败: {ex.Message}");
            }
            finally
            {
                if (myDB.State == ConnectionState.Open)
                    myDB.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin admin = new AdminLogin();
            admin.Show();
            this.Hide();
        }
    }
}
