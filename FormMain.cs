using System;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public class FormMain : Form
    {
        private Button btnKH, btnVT, btnHD, btnCTHD, btnTruyVan;

        public FormMain()
        {
            Text = "Quan Ly Ban Hang - Main";
            Width = 600; Height = 220;
            StartPosition = FormStartPosition.CenterScreen;

            btnKH = new Button() { Text = "Quản lý Khách hàng", Left = 20, Top = 20, Width = 200 };
            btnVT = new Button() { Text = "Quản lý Vật tư", Left = 320, Top = 20, Width = 200 };
            btnHD = new Button() { Text = "Quản lý Hóa đơn", Left = 20, Top = 70, Width = 200 };
            btnCTHD = new Button() { Text = "Chi tiết Hóa đơn", Left = 320, Top = 70, Width = 200 };
            btnTruyVan = new Button() { Text = "Truy vấn (các câu)", Left = 170, Top = 120, Width = 240 };

            btnKH.Click += (s,e) => new FormKhachHang().Show();
            btnVT.Click += (s,e) => new FormVatTu().Show();
            btnHD.Click += (s,e) => new FormHoaDon().Show();
            btnCTHD.Click += (s,e) => new FormCTHD().Show();
            btnTruyVan.Click += (s,e) => new FormTruyVan().Show();

            Controls.AddRange(new Control[] { btnKH, btnVT, btnHD, btnCTHD, btnTruyVan });
        }
    }
}
