using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormHoaDon : Form
    {
        private DataGridView dgv;
        private TextBox txtMa;
        private DateTimePicker dtp;
        private ComboBox cboKhach;
        private Button btnAdd, btnReload, btnCalcTotal;

        public FormHoaDon()
        {
            Text = "Quản lý Hóa đơn";
            Width = 700; Height = 420;
            StartPosition = FormStartPosition.CenterScreen;

            dgv = new DataGridView() { Left = 10, Top = 10, Width = 660, Height = 220, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            Label l1 = new Label() { Text = "Mã HD:", Left = 10, Top = 250 };
            txtMa = new TextBox() { Left = 80, Top = 247, Width = 150 };
            Label l2 = new Label() { Text = "Ngày:", Left = 250, Top = 250 };
            dtp = new DateTimePicker() { Left = 300, Top = 245, Width = 200 };
            Label l3 = new Label() { Text = "Khách:", Left = 10, Top = 290 };
            cboKhach = new ComboBox() { Left = 80, Top = 287, Width = 300 };

            btnAdd = new Button() { Text = "Thêm", Left = 400, Top = 250, Width = 120 };
            btnReload = new Button() { Text = "Tải lại", Left = 540, Top = 250, Width = 120 };
            btnCalcTotal = new Button() { Text = "Tính Tổng (cập nhật TONGTG)", Left = 200, Top = 330, Width = 300 };

            btnAdd.Click += BtnAdd_Click;
            btnReload.Click += (s,e) => LoadData();
            btnCalcTotal.Click += BtnCalcTotal_Click;

            Controls.AddRange(new Control[] { dgv, l1, txtMa, l2, dtp, l3, cboKhach, btnAdd, btnReload, btnCalcTotal });

            LoadCombo();
            LoadData();
        }

        private void LoadCombo()
        {
            var dt = DatabaseHelper.GetData("SELECT MAKH, TENKH FROM KHACHHANG");
            cboKhach.DisplayMember = "TENKH";
            cboKhach.ValueMember = "MAKH";
            cboKhach.DataSource = dt;
        }

        private void LoadData()
        {
            dgv.DataSource = DatabaseHelper.GetData("SELECT * FROM HOADON");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var makh = cboKhach.SelectedValue?.ToString() ?? "";
            string sql = $"INSERT INTO HOADON(MAHD, NGAY, MAKH) VALUES('{txtMa.Text}', '{dtp.Value:yyyy-MM-dd}', '{makh}')";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã thêm hóa đơn.");
        }

        private void BtnCalcTotal_Click(object sender, EventArgs e)
        {
            // Update TONGTG in HOADON by summing GIABAN*SL from CTHD
            string sql = @"
                UPDATE HOADON SET TONGTG = ISNULL((
                    SELECT SUM(c.GIABAN * c.SL) FROM CTHD c WHERE c.MAHD = HOADON.MAHD
                ),0)
            ";
            DatabaseHelper.Execute(sql);
            LoadData();
            MessageBox.Show("Đã cập nhật TONGTG cho các hóa đơn.");
        }
    }
}
