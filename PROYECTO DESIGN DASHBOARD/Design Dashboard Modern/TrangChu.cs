﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace Design_Dashboard_Modern
{
    public partial class TrangChu : Form
    {
        int idAccountLogin = 0;
        public TrangChu()
        {
            InitializeComponent();
        }
        public TrangChu(int idAccount)
        {
            InitializeComponent();
            this.idAccountLogin = idAccount;
        }
        int LayQuyen()
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            Account acc = db.Accounts.FirstOrDefault(x => x.Account_id == idAccountLogin);
            return Convert.ToInt32(acc.Pos_id);
        }
        void LoadDataNhanVien()
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            var result = from d in db.Accounts
                         join e in db.Logins on d.Account_id equals e.Account_id
                         select new { d.Account_id, d.Account_name, d.Pos_id, d.Account_address, d.Account_phone, d.Account_DOB, d.Account_Email, d.Account_status, d.Account_avatar };
            dgvAccount.DataSource = result.ToList();
            dgvAccount.Columns[0].HeaderText = "Mã NV";
            dgvAccount.Columns[1].HeaderText = "Tên NV";
            dgvAccount.Columns[2].HeaderText = "Vị Trí";
            dgvAccount.Columns[3].HeaderText = "Địa Chỉ";
            dgvAccount.Columns[4].HeaderText = "Số ĐT";
            dgvAccount.Columns[5].HeaderText = "Ngày Sinh";
            dgvAccount.Columns[6].HeaderText = "Email";
            dgvAccount.Columns[7].HeaderText = "Trạng Thái";
            dgvAccount.Columns[8].HeaderText = "Avatar";
            List<Position> list = db.Positions.ToList();
            try
            {
                cmbPosition.DataSource = list;
                cmbPosition.DisplayMember = "Pos_name";
                cmbPosition.ValueMember = "Pos_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tải dữ liệu của Position", ex.ToString());
            }
            cmbTimKiemNV.Items.Add("Mã NV");
            cmbTimKiemNV.Items.Add("Tên NV");
            cmbTimKiemNV.Items.Add("Vị Trí");
            cmbTimKiemNV.Items.Add("Số NV");
            cmbTimKiemNV.Items.Add("Địa Chỉ");
            cmbTimKiemNV.Items.Add("Ngày Sinh");
            cmbTimKiemNV.Items.Add("Email");
            cmbTimKiemNV.SelectedItem = "Mã NV";
            txtIDNV.Enabled = false;
        }
        void LoadDataKhachHang()
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            var result = from c in db.Customers
                         select new { c.Cus_id, c.Cus_name, c.Cus_phone, c.Cus_address, c.Cus_email, c.Cus_Point, c.Cus_level, c.Cus_DOB };
            dgvCustomer.DataSource = result.ToList();
            dgvCustomer.Columns[0].HeaderText = "Mã KH";
            dgvCustomer.Columns[1].HeaderText = "Tên KH";
            dgvCustomer.Columns[2].HeaderText = "SĐT KH";
            dgvCustomer.Columns[3].HeaderText = "Địa Chỉ KH";
            dgvCustomer.Columns[4].HeaderText = "Mail";
            dgvCustomer.Columns[5].HeaderText = "Điểm TL";
            dgvCustomer.Columns[6].HeaderText = "Level";
            dgvCustomer.Columns[7].HeaderText = "Ngày Sinh";
            List<Level> list = db.Levels.ToList();
            try
            {
                cmbLevel.DataSource = list;
                cmbLevel.DisplayMember = "Level_name";
                cmbLevel.ValueMember = "Cus_level";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tải dữ liệu của Level", ex.ToString());
            }
            cmbTimKiem.Items.Add("Mã KH");
            cmbTimKiem.Items.Add("Tên KH");
            cmbTimKiem.Items.Add("Số ĐT");
            cmbTimKiem.Items.Add("Địa Chỉ");
            cmbTimKiem.Items.Add("Email");
            cmbTimKiem.Items.Add("Level");
            cmbTimKiem.Items.Add("Ngày Sinh");
            cmbTimKiem.SelectedItem = "Mã KH";
            txtCustomer_id.Enabled = false;
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Maximizar.Visible = false;
            Restaurar.Visible = true;
        }

        private void Restaurar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Restaurar.Visible = false;
            Maximizar.Visible = true;
        }

        private void MenuSidebar_Click(object sender, EventArgs e)
        {
            if (Sidebar.Width == 270)
            {
                Sidebar.Visible = false;
                Sidebar.Width = 68;
                SidebarWrapper.Width = 90;
                LineaSidebar.Width = 52;
                AnimacionSidebar.Show(Sidebar);
                Wrapper.Width = 1047;
                Wrapper.Location = new Point(90, 80);
            }
            else
            {
                Sidebar.Visible = false;
                Sidebar.Width = 270;
                SidebarWrapper.Width = 300;
                LineaSidebar.Width = 252;
                AnimacionSidebarBack.Show(Sidebar);
                Wrapper.Width = 878;
                Wrapper.Location = new Point(300, 80);
            }
        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            Temporizador.Stop();

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnTrangChu.Height;
            SidePanel.Top = btnTrangChu.Top + 25;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnSanPham.Height;
            SidePanel.Top = btnSanPham.Top + 25;
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            int Pos_id = LayQuyen();
            if (Pos_id == 1)
            {
                MessageBox.Show("Bạn không có quyền hiển thị bán hàng!");
            }
            else
            {
                SidePanel.Height = btnBanHang.Height;
                SidePanel.Top = btnBanHang.Top + 25;
                BanHang.BringToFront();
                LoadDataBanHang();
            }

        }

        void LoadDataBanHang()
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            var result = from o in db.Orders
                         join od in db.Order_detail on o.Order_id equals od.Order_id
                         join a in db.Accounts on o.Account_id equals a.Account_id
                         join c in db.Customers on o.Cus_id equals c.Cus_id
                         select new { o.Order_id, a.Account_name, c.Cus_name, c.Cus_phone, od.Pro_id, od.Quantity, od.order_total_price };
            dgvOrder.DataSource = result.ToList();
        }


        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            int Pos_id = LayQuyen();
            if (Pos_id == 1)
            {
                MessageBox.Show("Bạn Không có quyền!");
            }
            else
            {
                SidePanel.Height = btnXuatKho.Height;
                SidePanel.Top = btnXuatKho.Top + 25;
            }

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            int Pos_id = LayQuyen();
            if (Pos_id == 1)
            {
                MessageBox.Show("Bạn Không có quyền!");
            }
            else
            {
                SidePanel.Height = btnKhachHang.Height;
                SidePanel.Top = btnKhachHang.Top + 25;
                PanelDSKhachHang.BringToFront();
                LoadDataKhachHang();
            }
        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnNhanVien.Height;
            SidePanel.Top = btnNhanVien.Top + 25;
            PanelGiaoDienNV.BringToFront();
            LoadDataNhanVien();
            if (LayQuyen() == 1 || LayQuyen() == 2)
            {
                PanelButtonNV.Enabled = false;
            }
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnThongke.Height;
            SidePanel.Top = btnThongke.Top + 25;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnThongke.Height;
            SidePanel.Top = btnThongke.Top + 25;

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            LoadDataKhachHang();
        }

        private void PanelDSKhachHang_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnThemCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string Fullname = txtFullName.Text;
                string SDT = txtSDT.Text;
                string DiaChi = txtAddress.Text;
                string Email = txtEmail.Text;
                double Point = 0;
                int Cap = Convert.ToInt32(cmbLevel.SelectedValue.ToString());
                DateTime DOB = DateDOB.Value;
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                Customer new_cus = new Customer();
                new_cus.Cus_name = Fullname;
                new_cus.Cus_phone = SDT;
                new_cus.Cus_address = DiaChi;
                new_cus.Cus_email = Email;
                new_cus.Cus_Point = Point;
                new_cus.Cus_level = Cap;
                new_cus.Cus_DOB = DOB;
                db.Customers.Add(new_cus);
                db.SaveChanges();
                LoadDataKhachHang();
                MessageBox.Show("Thêm Khách Hàng Thành Công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm Khách Hàng Thất Bại!");

            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int idCus = Convert.ToInt32(txtCustomer_id.Text);
                string Fullname = txtFullName.Text;
                string SDT = txtSDT.Text;
                string DiaChi = txtAddress.Text;
                string Email = txtEmail.Text;
                int Cap = Convert.ToInt32(cmbLevel.SelectedValue.ToString());
                DateTime DOB = DateDOB.Value;
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                Customer cus = db.Customers.FirstOrDefault(x => x.Cus_id == idCus);
                cus.Cus_name = Fullname;
                cus.Cus_phone = SDT;
                cus.Cus_address = DiaChi;
                cus.Cus_email = Email;
                cus.Cus_level = Cap;
                cus.Cus_DOB = DOB;
                db.SaveChanges();
                LoadDataKhachHang();
                MessageBox.Show("Cập Nhật Khách Hàng Thành Công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Cập Nhật Khách Hàng Thất Bại!");
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomer_id.Text = dgvCustomer[0, dgvCustomer.CurrentRow.Index].Value.ToString();
            txtFullName.Text = dgvCustomer[1, dgvCustomer.CurrentRow.Index].Value.ToString();
            txtSDT.Text = dgvCustomer[2, dgvCustomer.CurrentRow.Index].Value.ToString();
            txtAddress.Text = dgvCustomer[3, dgvCustomer.CurrentRow.Index].Value.ToString();
            txtEmail.Text = dgvCustomer[4, dgvCustomer.CurrentRow.Index].Value.ToString();
            cmbLevel.SelectedValue = dgvCustomer[6, dgvCustomer.CurrentRow.Index].Value;
            DateDOB.Value = Convert.ToDateTime(dgvCustomer[7, dgvCustomer.CurrentRow.Index].Value);

        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int idCus = Convert.ToInt32(txtCustomer_id.Text);
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                Customer cus = db.Customers.FirstOrDefault(x => x.Cus_id == idCus);
                db.Customers.Remove(cus);
                db.SaveChanges();
                LoadDataKhachHang();
                MessageBox.Show("Xóa Khách Hàng Thành Công!");
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa Khách Hàng Thất bại!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keySearching = txtSearch.Text;
            string typeSearching = cmbTimKiem.SelectedItem.ToString();
            if (typeSearching == null)
            {
                MessageBox.Show("Bạn chưa chọn loại tìm kiếm!");
            }
            else
            if (typeSearching.Equals("Mã KH"))
            {
                int idCus = Convert.ToInt32(txtSearch.Text);
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_id == idCus);
                dgvCustomer.DataSource = Customer.ToList();
            }
            else if (typeSearching.Equals("Tên KH"))
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_name.Contains(keySearching));
                dgvCustomer.DataSource = Customer.ToList();
            }
            else if (typeSearching.Equals("Số ĐT"))
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_phone == keySearching);
                dgvCustomer.DataSource = Customer.ToList();
            }
            else if (typeSearching.Equals("Địa Chỉ"))
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_address.Contains(keySearching));
                dgvCustomer.DataSource = Customer.ToList();
            }
            else if (typeSearching.Equals("Email"))
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_email.Contains(keySearching));
                dgvCustomer.DataSource = Customer.ToList();
            }
            else if (typeSearching.Equals("Level"))
            {
                int level = Convert.ToInt32(keySearching);
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var Customer = db.Customers.Where(x => x.Cus_level == level);
                dgvCustomer.DataSource = Customer.ToList();
            }
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            try
            {
                string FullNameNV = txtFullnameNV.Text;
                string SDTNV = txtSDTNV.Text;
                string DiaChiNV = txtDiaChiNV.Text;
                string EmailNV = txtEmailNV.Text;
                DateTime DOBNV = DateDOBNV.Value;
                string UsernameNV = txtUsername.Text;
                int Pos_id = Convert.ToInt32(cmbPosition.SelectedValue.ToString());
                if (FullNameNV.Length == 0 || SDTNV.Length == 0 || DiaChiNV.Length == 0 || EmailNV.Length == 0 || UsernameNV.Length == 0 || Pos_id < 0)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ các trường thông tin!");
                }
                else
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Login lg = new Login();
                    lg.Username = UsernameNV;
                    lg.Password = getMd5Hash("admin123");
                    db.Logins.Add(lg);
                    db.SaveChanges();
                    Login timkiem = db.Logins.FirstOrDefault(x => x.Username == UsernameNV);
                    int idAccount = timkiem.Account_id;
                    Account acc = new Account();
                    acc.Account_id = idAccount;
                    acc.Account_name = FullNameNV;
                    acc.Account_phone = SDTNV;
                    acc.Account_Email = EmailNV;
                    acc.Account_address = DiaChiNV;
                    acc.Account_DOB = DOBNV;
                    acc.Pos_id = Pos_id;
                    acc.Account_status = true;
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    LoadDataNhanVien();
                    MessageBox.Show("Thêm Nhân Viên Thành Công!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Thêm Nhân Viên Thất Bại!");
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                int idAccount = Convert.ToInt32(txtIDNV.Text);
                if (idAccount == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!");
                }
                else
                {
                    string PasswordMacDinh = "admin123";
                    string PasswordMaHoa = getMd5Hash(PasswordMacDinh);
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Login lg = db.Logins.FirstOrDefault(x => x.Account_id == idAccount);
                    lg.Password = PasswordMaHoa;
                    db.SaveChanges();
                    MessageBox.Show("Reset Mật Khẩu Thành Công!");
                }
            }
            catch
            {
                MessageBox.Show("Reset Mật Khẩu Thất Bại!");
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

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            try
            {
                int idAccount = Convert.ToInt32(txtIDNV.Text);
                if (idAccount == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!");
                }
                else
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Account acc = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
                    db.Accounts.Remove(acc);
                    db.SaveChanges();
                    Login lg = db.Logins.FirstOrDefault(x => x.Account_id == idAccount);
                    db.Logins.Remove(lg);
                    db.SaveChanges();
                    MessageBox.Show("Xóa Thành Công!");
                    LoadDataNhanVien();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Xóa Thất Bại!");
            }
        }

        private void btnCapNhatNV_Click(object sender, EventArgs e)
        {
            try
            {
                int idAccount = Convert.ToInt32(txtIDNV.Text);
                if (idAccount == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!");
                }
                else
                {
                    string FullNameNV = txtFullnameNV.Text;
                    string SDTNV = txtSDTNV.Text;
                    string DiaChiNV = txtDiaChiNV.Text;
                    string EmailNV = txtEmailNV.Text;
                    DateTime DOBNV = DateDOBNV.Value;
                    string UsernameNV = txtUsername.Text;
                    int Pos_id = Convert.ToInt32(cmbPosition.SelectedValue.ToString());
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Account acc = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
                    acc.Account_name = FullNameNV;
                    acc.Account_phone = SDTNV;
                    acc.Account_Email = EmailNV;
                    acc.Account_address = DiaChiNV;
                    acc.Account_DOB = DOBNV;
                    acc.Pos_id = Pos_id;
                    db.SaveChanges();
                    MessageBox.Show("Cập Nhật thành công!");
                    LoadDataNhanVien();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cập Nhật thất bại!");
            }

        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {

                int idAccount = Convert.ToInt32(txtIDNV.Text);
                if (idAccount == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!");
                }
                else
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Account acc = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
                    if (acc.Account_status == true)
                    {
                        acc.Account_status = false;
                        db.SaveChanges();
                        MessageBox.Show("Đã vô hiệu hóa thành công!");
                        LoadDataNhanVien();
                    }
                    else
                    {
                        acc.Account_status = true;
                        db.SaveChanges();
                        MessageBox.Show("Đã kích hoạt thành công!");
                        LoadDataNhanVien();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDNV.Text = dgvAccount[0, dgvAccount.CurrentRow.Index].Value.ToString();
            txtFullnameNV.Text = dgvAccount[1, dgvAccount.CurrentRow.Index].Value.ToString();
            cmbPosition.SelectedValue = dgvAccount[2, dgvAccount.CurrentRow.Index].Value;
            txtDiaChiNV.Text = dgvAccount[3, dgvAccount.CurrentRow.Index].Value.ToString();
            txtSDTNV.Text = dgvAccount[4, dgvAccount.CurrentRow.Index].Value.ToString();
            DateDOBNV.Value = Convert.ToDateTime(dgvAccount[5, dgvAccount.CurrentRow.Index].Value);
            txtEmailNV.Text = dgvAccount[6, dgvAccount.CurrentRow.Index].Value.ToString();
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            int idAccount = Convert.ToInt32(txtIDNV.Text);
            Account acc = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
            Login lg = db.Logins.FirstOrDefault(y => y.Account_id == idAccount);
            string img = acc.Account_avatar.ToString();
            txtUsername.Text = lg.Username.ToString();
            BoxAvatar.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Data\\" + img);
            BoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            if (idAccount == idAccountLogin)
            {
                PanelButtonNV.Enabled = true;
            }
            else
            {
                PanelButtonNV.Enabled = false;
            }
        }

        private void btnRenewNV_Click(object sender, EventArgs e)
        {
            LoadDataNhanVien();
        }

        private void btnTimKiemNV_Click(object sender, EventArgs e)
        {
            string keySearching = txtTimKiemNV.Text;
            if (keySearching.Length == 0)
            {
                LoadDataNhanVien();
            }
            else
            {
                string typeSearching = cmbTimKiemNV.SelectedItem.ToString();
                if (typeSearching == null)
                {
                    MessageBox.Show("Bạn chưa chọn loại tìm kiếm!");
                }
                else
                if (typeSearching.Equals("Mã NV"))
                {
                    int idCus = Convert.ToInt32(txtSearch.Text);
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    var Account = db.Accounts.Where(x => x.Account_id == idCus);
                    dgvAccount.DataSource = Account.ToList();
                }
                else if (typeSearching.Equals("Tên NV"))
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    var Account = db.Accounts.Where(x => x.Account_name.Contains(keySearching));
                    dgvAccount.DataSource = Account.ToList();
                }
                else if (typeSearching.Equals("Vị Trí"))
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    Position Pos = db.Positions.FirstOrDefault(x => x.Pos_name.Contains(keySearching));
                    int Pos_id = Pos.Pos_id;
                    var Account = db.Accounts.Where(x => x.Pos_id == Pos_id);
                    dgvAccount.DataSource = Account.ToList();
                }
                else if (typeSearching.Equals("Số NV"))
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    var Account = db.Accounts.Where(x => x.Account_phone.Contains(keySearching));
                    dgvAccount.DataSource = Account.ToList();
                }
                else if (typeSearching.Equals("Địa Chỉ"))
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    var Account = db.Accounts.Where(x => x.Account_Email.Contains(keySearching));
                    dgvAccount.DataSource = Account.ToList();
                }
                else if (typeSearching.Equals("Email"))
                {
                    QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                    var Account = db.Accounts.Where(x => x.Account_Email.Contains(keySearching));
                    dgvAccount.DataSource = Account.ToList();
                }
            }

        }

        private void btnUpdateAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog uploadFileSteam = new OpenFileDialog();
            uploadFileSteam.Filter = "JPG|*.jpg";
            uploadFileSteam.FilterIndex = 2;
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            int idAccount = Convert.ToInt32(txtIDNV.Text);
            if (idAccount == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên muốn Upload!");
            }
            else
            {
                Account getOldNameImg = db.Accounts.FirstOrDefault(x => x.Account_id == idAccount);
                string imgAccountOld = getOldNameImg.Account_avatar.ToString();
                if (uploadFileSteam.ShowDialog() == DialogResult.OK)
                {
                    if (imgAccountOld.Equals("default-avatar.jpg") == false)
                    {
                        string FilePath = Directory.GetCurrentDirectory() + "\\Data\\" + imgAccountOld;
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                    }
                    string FileUp = Directory.GetCurrentDirectory() + "\\Data\\" + uploadFileSteam.SafeFileName;
                    if (File.Exists(FileUp))
                    {
                        File.Delete(FileUp);
                    }
                    File.Copy(uploadFileSteam.FileName, Directory.GetCurrentDirectory() + "\\Data\\" + uploadFileSteam.SafeFileName);
                    getOldNameImg.Account_avatar = uploadFileSteam.SafeFileName;
                    db.SaveChanges();
                    BoxAvatar.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Data\\" + uploadFileSteam.SafeFileName);
                    MessageBox.Show("Upload Ảnh Thành Công!");
                }
                else
                {
                    MessageBox.Show("Có Lỗi xảy ra trong quá trình Upload Ảnh!");
                }
            }
        }

        private void txtTimKiemNV_Enter(object sender, EventArgs e)
        {

        }


        void LoadProductsOrder()
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            var result = from p in db.Products
                         select new { p.Pro_id, p.Pro_name, p.Pro_quantity, p.Pro_Price };
            dgvProductOrder.DataSource = result.ToList();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            String key = bunifuTextbox1.Text;
            if (key.Length == 0)
            {
                LoadProductsOrder();
            }
            else
            {
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                var result = from
                    c in db.Products
                             where c.Pro_name.Contains(key)
                             select new
                             {
                                 c.Pro_id,
                                 c.Pro_name,
                                 c.Pro_Price,
                                 c.Pro_quantity
                             };
                dgvProductOrder.DataSource = result.ToList();

            }
            
        }

        private void gunaDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel11_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel16_Click(object sender, EventArgs e)
        {

        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if(txtTenVL.Text.Length != 0  && txtsdtVL.Text.Length != 0 && txtDiaChiVl.Text.Length != 0) 
            {
                string TenKhachVL = txtTenVL.Text;
                string SDTKhachVL = txtsdtVL.Text;
                string DiaChiVL = txtDiaChiVl.Text;
                DateTime DOBVL = dtThoiGianVL.Value;
                QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
                string SDT = txtsdtVL.Text;
                Customer cus = db.Customers.Where(x => x.Cus_phone.ToString() == SDT);
                if ()
                {

                }   
                else
                {
                    Customer cus_new = new Customer();
                    cus_new.Cus_name = TenKhachVL;
                    cus_new.Cus_phone = SDTKhachVL;
                    cus_new.Cus_address = DiaChiVL;
                    cus_new.Cus_DOB = DOBVL;
                    cus_new.Cus_level = 1;
                    cus_new.Cus_Point = 0;
                    db.Customers.Add(cus_new);
                    db.SaveChanges();
                }    
                Order or = new Order();
                or.Order_note = txtNoteOrder.Text;
                or.Cus_id 



            } else
            {
                MessageBox.Show("Lỗi mẹ rồi !");
            }

        }

        List<Order_Item> list = new List<Order_Item>();
        private void dgvProductOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSPOrder.Text = dgvProductOrder[0, dgvProductOrder.CurrentRow.Index].Value.ToString();
            txtNamePro.Text = dgvProductOrder[1, dgvProductOrder.CurrentRow.Index].Value.ToString();
            Pro_Price.Text = dgvProductOrder[2, dgvProductOrder.CurrentRow.Index].Value.ToString();
            txtQuantity.Text = dgvProductOrder[3, dgvProductOrder.CurrentRow.Index].Value.ToString();   
        }
        double TongTien = 0;
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            Order_Item oi = new Order_Item();
            oi.Pro_id1 = txtSPOrder.Text;
            oi.Pro_name1 = txtNamePro.Text;
            oi.Pro_Price1 = Convert.ToDouble(Pro_Price.Text);
            oi.Pro_quantity1 = Convert.ToInt32(txtQuantity.Text);
            TongTien += oi.Pro_Price1 * Convert.ToDouble(oi.Pro_quantity1);
            list.Add(oi);
            dgvOrder.DataSource = list.ToList();
            lbTongTIen.Text = TongTien.ToString();

            double valueTongTien = Convert.ToDouble(TongTien.ToString());
            double GiamGia = Convert.ToDouble(lbGiamGia.Text);
            double ThanhToanTien = valueTongTien - (valueTongTien * GiamGia /100);
            lbThanhToanTien.Text = Convert.ToString(ThanhToanTien) + " đ";
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {

        }

        private void btnTimKiemKHOrder_Click(object sender, EventArgs e)
        {
            QuanLyBanHang_DoAnEntities db = new QuanLyBanHang_DoAnEntities();
            string keySDT = txtOrder_TimSDTKH.text;
            Customer cus = db.Customers.FirstOrDefault(x => x.Cus_phone == keySDT);
            txtTenVL.Text = Convert.ToString(cus.Cus_name);
            dtThoiGianVL.Value = Convert.ToDateTime(cus.Cus_DOB);
            txtsdtVL.Text = Convert.ToString(cus.Cus_phone);
            txtDiaChiVl.Text = Convert.ToString(cus.Cus_address);
            lbGiamGia.Text = Convert.ToString(cus.Cus_Point) + " %";
            double TongTien = Convert.ToDouble(lbTongTIen.Text);
            double GiamGia = Convert.ToDouble(cus.Cus_Point);
            double ThanhToanTien = TongTien - (TongTien * GiamGia / 100);
            lbThanhToanTien.Text = Convert.ToString(ThanhToanTien) + " đ";
        }
    }
}



