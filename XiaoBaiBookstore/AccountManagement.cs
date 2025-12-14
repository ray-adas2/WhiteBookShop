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
    public partial class AccountManagement : Form
    {
        private FormMove _forMove;
        public AccountManagement()
        {
            InitializeComponent();

            _forMove = new FormMove(this);
        }

        static string DBKey = "Data Source=RAY;Initial Catalog=WhiteBookShopDB;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";//数据库密钥

        SqlConnection myDB = new SqlConnection(DBKey);//创建数据库连接对象

        private void label4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Books login = new Books();
            login.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Users login = new Users();
            login.Show();
            this.Hide();
        }

        private void AccountManagement_Load(object sender, EventArgs e)
        {
            myDB.Open();
            SqlDataAdapter sdaBook = new SqlDataAdapter("select sum(BQty) from BookTb1",myDB);
            DataTable dtBook = new DataTable();
            sdaBook.Fill(dtBook);
            BookStockLbl.Text = dtBook.Rows[0][0].ToString();

            SqlDataAdapter sdaAmount = new SqlDataAdapter("select sum(Amount) from BillTb1", myDB);
            DataTable dtAmount = new DataTable();
            sdaAmount.Fill(dtAmount);
            AmountLbl.Text = dtAmount.Rows[0][0].ToString();

            SqlDataAdapter sdaUser = new SqlDataAdapter("select count(*) from UserTb1", myDB);
            DataTable dtUser = new DataTable();
            sdaUser.Fill(dtUser);
            UserTotalLbl.Text = dtUser.Rows[0][0].ToString();


            myDB.Close();
        }
    }
}
