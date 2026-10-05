using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormVatTu : Form
    {
        private DataGridView dgv;
        private TextBox txtMa, txtTen, txtDVT, txtGia, txtSL;
        private Button btnAdd, btnUpdate, btnDelete, btnReload;

        public FormVatTu()
        {
            Text = "Quản lý Vật tư";
            Width = 900; Height = 520;
            StartPosition = FormStartPosition.CenterScreen;

            dgv = new DataGridView() { Left = 10, Top = 10, Width = 860, Height = 300, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            Label l1 = new Label() { Text = "Mã VT:", Left = 10, Top = 330 };
            txtMa = new TextBox() { Left = 80, Top = 327, Width = 200 };
            Label l2 = new Label() { Text = "Tên VT:", Left = 300, Top = 330 };
            txtTen = new TextBox() { Left = 370, Top = 327, Width = 200 };
            Label l3 = new Label() { Text = "DVT:", Left = 10, Top = 370 };
            txtDVT = new TextBox() { Left = 80, Top = 367, Width = 200 };
            Label l4 = new Label() { Text = "Giá mua:", Left = 300, Top = 370 };
            txtGia = new TextBox() { Left = 370, Top = 367, Width = 200 };
            Label l5 = new Label() { Text = "SL tồn:", Left = 10, Top = 410 };
            txtSL = new TextBox() { Left = 80, Top = 407, Width = 200 };

            btnAdd = new Button() { Text = "Thêm", Left = 600, Top = 330, Width = 120 };
            btnUpdate = new Button() { Text = "Sửa", Left = 600, Top = 370, Width = 120 };
            btnDelete = new Button() { Text = "Xóa", Left = 740, Top = 330, Width = 120 };
            btnReload = new Button() { Text = "Tải lại", Left = 740, Top = 370, Width = 120 };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnReload.Click += (s,e) => LoadData();
            dgv.CellClick += Dgv_CellClick;

            Controls.AddRange(new Control[] { dgv, l1, txtMa, l2, txtTen, l3, txtDVT, l4, txtGia, l5, txtSL, btnAdd, btnUpdate, btnDelete, btnReload });

            LoadData();
        }

        private void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData("SELECT * FROM VATTU");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO VATTU(MAVT,TENVT,DVT,GIAMUA,SLTON) VALUES('{txtMa.Text}', N'{txtTen.Text}', N'{txtDVT.Text}', {txtGia.Text}, {txtSL.Text})";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã thêm.");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string sql = $@"UPDATE VATTU SET TENVT=N'{txtTen.Text}', DVT=N'{txtDVT.Text}', GIAMUA={txtGia.Text}, SLTON={txtSL.Text} WHERE MAVT='{txtMa.Text}'";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã cập nhật.");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM VATTU WHERE MAVT='{txtMa.Text}'";
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
            txtDVT.Text = row.Cells[2].Value?.ToString();
            txtGia.Text = row.Cells[3].Value?.ToString();
            txtSL.Text = row.Cells[4].Value?.ToString();
        }
    }
}
