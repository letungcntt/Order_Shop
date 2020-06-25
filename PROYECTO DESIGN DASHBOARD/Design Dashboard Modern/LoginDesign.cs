using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Threading;

namespace Design_Dashboard_Modern
{
    public partial class LoginDesign : Form
    {
        public LoginDesign()
        {
            InitializeComponent();
        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panelRegister_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnChangeViewRegis_Click_1(object sender, EventArgs e)
        {
            panelRegister.BringToFront();
            gunaTransition1.Show(panelRegister);
            PanelDangNhap.Hide();
        }

        private void BoxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BoxMinize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnChangeViewLogin_Click(object sender, EventArgs e)
        {
            PanelDangNhap.BringToFront();
            gunaTransition1.Show(PanelDangNhap);
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {
            string username = txtDangKyUsername.Text;
            string password = getMd5Hash(txtDangKyPassword.Text);
            string repassword = getMd5Hash(txtDangKyRePassword.Text);
            string fullname = txtFullname.Text;
            string address = txtAddress.Text;
            string phone = txtSDT.Text;
            if (password.Equals(repassword))
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                Login newLogin = new Login();
                newLogin.Username = username;
                newLogin.Password = password;
                db.Logins.Add(newLogin);
                db.SaveChanges();
                Login login = db.Logins.FirstOrDefault(x => x.Username == username);
                Account newAcc = new Account();
                newAcc.Account_id = login.Account_id;
                newAcc.Account_name = fullname;
                newAcc.Account_address = address;
                newAcc.Account_phone = phone;
                newAcc.Account_status = true;
                db.Accounts.Add(newAcc);
                db.SaveChanges();
                txtThongBao.Text = "Đăng ký thành công!";
            }
            else
            {
                txtThongBao.Text = "Mật khẩu không khớp nhau!";
            }

        }
        static string getMd5Hash(string input)
        { // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create(); // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes // and create a string.
            StringBuilder sBuilder = new StringBuilder(); // Loop through each byte of the hashed data // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = getMd5Hash(txtPassword.Text);
            if (username.Length == 0 && password.Length == 0)
            {
                txtThongBao.Text = "Vui lòng nhập tài khoản và mật khẩu!";
            }
            else if (username.Length == 0)
            {
                txtThongBao.Text = "Vui lòng nhập tài khoản!";
            }
            else if (password.Length == 0)
            {
                txtThongBao.Text = "Vui lòng nhập mật khẩu!";
            }
            else
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                int check = db.Logins.Count(x => x.Username == username && x.Password == password);
                if (check == 1)
                {
                    Login acc = db.Logins.FirstOrDefault(x => x.Username == username);
                    int idAccount = Convert.ToInt32(acc.Account_id.ToString());
                    Account getStatus = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
                    Boolean checkStatus = Convert.ToBoolean(getStatus.Account_status.ToString());
                    if (checkStatus == true)
                    {
                        this.Hide();
                        TrangChu tc = new TrangChu();
                        tc.ShowDialog();
                    }
                    else
                    {
                        txtThongBao.Text = "Tài khoản của bạn đã bị vô hiệu hóa!";
                    }

                }
                else
                {
                    txtThongBao.Text = "Đăng nhập thất bại!";
                }
            }
        }
    }
}
