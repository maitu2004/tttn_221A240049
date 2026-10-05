using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormKhachHang : Form
    {
        private DataGridView dgv;
        private TextBox txtMa, txtTen, txtDiaChi, txtDT, txtEmail;
        private Button btnAdd, btnUpdate, btnDelete, btnReload;

        public FormKhachHang()
        {
            Text = "Quản lý Khách hàng";
            Width = 900; Height = 520;
            StartPosition = FormStartPosition.CenterScreen;

            dgv = new DataGridView() { Left = 10, Top = 10, Width = 860, Height = 300, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            Label l1 = new Label() { Text = "Mã KH:", Left = 10, Top = 330 };
            txtMa = new TextBox() { Left = 80, Top = 327, Width = 200 };
            Label l2 = new Label() { Text = "Tên KH:", Left = 300, Top = 330 };
            txtTen = new TextBox() { Left = 370, Top = 327, Width = 200 };
            Label l3 = new Label() { Text = "Địa chỉ:", Left = 10, Top = 370 };
            txtDiaChi = new TextBox() { Left = 80, Top = 367, Width = 490 };
            Label l4 = new Label() { Text = "Điện thoại:", Left = 10, Top = 410 };
            txtDT = new TextBox() { Left = 80, Top = 407, Width = 200 };
            Label l5 = new Label() { Text = "Email:", Left = 300, Top = 410 };
            txtEmail = new TextBox() { Left = 370, Top = 407, Width = 200 };

            btnAdd = new Button() { Text = "Thêm", Left = 600, Top = 330, Width = 120 };
            btnUpdate = new Button() { Text = "Sửa", Left = 600, Top = 370, Width = 120 };
            btnDelete = new Button() { Text = "Xóa", Left = 740, Top = 330, Width = 120 };
            btnReload = new Button() { Text = "Tải lại", Left = 740, Top = 370, Width = 120 };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnReload.Click += (s,e) => LoadData();
            dgv.CellClick += Dgv_CellClick;

            Controls.AddRange(new Control[] { dgv, l1, txtMa, l2, txtTen, l3, txtDiaChi, l4, txtDT, l5, txtEmail, btnAdd, btnUpdate, btnDelete, btnReload });

            LoadData();
        }

        private void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData("SELECT * FROM KHACHHANG");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO KHACHHANG(MAKH,TENKH,DIACHI,DT,EMAIL) VALUES('{txtMa.Text}', N'{txtTen.Text}', N'{txtDiaChi.Text}', '{txtDT.Text}', '{txtEmail.Text}')";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã thêm.");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string sql = $@"UPDATE KHACHHANG SET TENKH=N'{txtTen.Text}', DIACHI=N'{txtDiaChi.Text}', DT='{txtDT.Text}', EMAIL='{txtEmail.Text}' WHERE MAKH='{txtMa.Text}'";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã cập nhật.");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM KHACHHANG WHERE MAKH='{txtMa.Text}'";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã xóa.");
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgv.Rows[e.RowIndex];
            txtMa.Text = row.Cells[0].Value?.ToString();
            txtTen.Text = row.Cells[1].Value?.ToString();
            txtDiaChi.Text = row.Cells[2].Value?.ToString();
            txtDT.Text = row.Cells[3].Value?.ToString();
            txtEmail.Text = row.Cells[4].Value?.ToString();
        }
    }
}
