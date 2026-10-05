using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormCTHD : Form
    {
        private DataGridView dgv;
        private ComboBox cboHD, cboVT;
        private TextBox txtSL, txtGia, txtKM;
        private Button btnAdd, btnUpdate, btnDelete, btnReload;

        public FormCTHD()
        {
            Text = "Chi tiết Hóa đơn (CTHD)";
            Width = 900; Height = 520;
            StartPosition = FormStartPosition.CenterScreen;

            dgv = new DataGridView() { Left = 10, Top = 10, Width = 860, Height = 300, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            Label l1 = new Label() { Text = "Mã HD:", Left = 10, Top = 330 };
            cboHD = new ComboBox() { Left = 80, Top = 327, Width = 200 };
            Label l2 = new Label() { Text = "Vật tư:", Left = 300, Top = 330 };
            cboVT = new ComboBox() { Left = 370, Top = 327, Width = 200 };
            Label l3 = new Label() { Text = "SL:", Left = 10, Top = 370 };
            txtSL = new TextBox() { Left = 80, Top = 367, Width = 200 };
            Label l4 = new Label() { Text = "Giá bán:", Left = 300, Top = 370 };
            txtGia = new TextBox() { Left = 370, Top = 367, Width = 200 };
            Label l5 = new Label() { Text = "KM:", Left = 10, Top = 410 };
            txtKM = new TextBox() { Left = 80, Top = 407, Width = 200 };

            btnAdd = new Button() { Text = "Thêm", Left = 600, Top = 330, Width = 120 };
            btnUpdate = new Button() { Text = "Sửa", Left = 600, Top = 370, Width = 120 };
            btnDelete = new Button() { Text = "Xóa", Left = 740, Top = 330, Width = 120 };
            btnReload = new Button() { Text = "Tải lại", Left = 740, Top = 370, Width = 120 };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnReload.Click += (s,e)=> LoadData();
            dgv.CellClick += Dgv_CellClick;

            Controls.AddRange(new Control[] { dgv, l1, cboHD, l2, cboVT, l3, txtSL, l4, txtGia, l5, txtKM, btnAdd, btnUpdate, btnDelete, btnReload });

            LoadCombos();
            LoadData();
        }

        private void LoadCombos()
        {
            cboHD.DataSource = DatabaseHelper.GetData("SELECT MAHD FROM HOADON");
            cboHD.DisplayMember = "MAHD";
            cboHD.ValueMember = "MAHD";

            var dt = DatabaseHelper.GetData("SELECT MAVT, TENVT FROM VATTU");
            cboVT.DisplayMember = "TENVT";
            cboVT.ValueMember = "MAVT";
            cboVT.DataSource = dt;
        }

        private void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData("SELECT * FROM CTHD");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO CTHD(MAHD, MAVT, SL, KHUYENMAI, GIABAN) VALUES('{cboHD.Text}', '{cboVT.SelectedValue}', {txtSL.Text}, {txtKM.Text}, {txtGia.Text})";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã thêm chi tiết.");
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE CTHD SET SL={txtSL.Text}, KHUYENMAI={txtKM.Text}, GIABAN={txtGia.Text} WHERE MAHD='{cboHD.Text}' AND MAVT='{cboVT.SelectedValue}'";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã cập nhật.");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM CTHD WHERE MAHD='{cboHD.Text}' AND MAVT='{cboVT.SelectedValue}'";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã xóa.");
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgv.Rows[e.RowIndex];
            cboHD.Text = row.Cells[0].Value?.ToString();
            cboVT.SelectedValue = row.Cells[1].Value?.ToString();
            txtSL.Text = row.Cells[2].Value?.ToString();
            txtKM.Text = row.Cells[3].Value?.ToString();
            txtGia.Text = row.Cells[4].Value?.ToString();
        }
    }
}
