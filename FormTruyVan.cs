using System;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormTruyVan : Form
    {
        private DataGridView dgv;
        private Button btn1, btn2, btn7;

        public FormTruyVan()
        {
            Text = "Truy vấn - Bài 2 (một số câu mẫu)";
            Width = 900; Height = 520;
            StartPosition = FormStartPosition.CenterScreen;

            dgv = new DataGridView() { Left = 10, Top = 10, Width = 860, Height = 360, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            btn1 = new Button() { Text = "Câu 1: KH ở Tân Bình", Left = 10, Top = 390, Width = 260 };
            btn2 = new Button() { Text = "Câu 2: KH chưa có DT", Left = 290, Top = 390, Width = 260 };
            btn7 = new Button() { Text = "Câu 7: Vật tư có từ 'Gạch'", Left = 570, Top = 390, Width = 260 };

            btn1.Click += (s,e)=> dgv.DataSource = DatabaseHelper.GetData("SELECT MAKH, TENKH, DIACHI, DT, EMAIL FROM KHACHHANG WHERE DIACHI = N'\u0054\u00e2n Bình' OR DIACHI = N' Tân Bình' OR DIACHI = N'Tân Bình'");
            btn2.Click += (s,e)=> dgv.DataSource = DatabaseHelper.GetData("SELECT MAKH, TENKH, DIACHI, EMAIL FROM KHACHHANG WHERE DT IS NULL OR LTRIM(RTRIM(DT)) = ''");
            btn7.Click += (s,e)=> dgv.DataSource = DatabaseHelper.GetData("SELECT MAVT, TENVT, DVT, GIAMUA FROM VATTU WHERE TENVT LIKE N'%Gạch%'");

            Controls.AddRange(new Control[] { dgv, btn1, btn2, btn7 });
        }
    }
}
