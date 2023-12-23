using System;
using System.Windows.Forms;

namespace QuanLyDoanhNghiepMililap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassWord.Text == "")
            {
                MessageBox.Show("Thông tin đăng nhập không đầy đủ ", "Thông báo ");
            }
            else
            {
                if (txtUserName.Text == "Nhom3" && txtPassWord.Text == "123456")
                {
                    MessageBox.Show("Bạn đăng nhập thành công ", "Thông báo ");
                    fChinh f = new fChinh();
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thông tin đăng nhập không đúng ", "Thông báo ");
                }
            }

        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự thoát khỏi chương trình ?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }

        }
    }
}
