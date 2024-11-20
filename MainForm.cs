using System;
using System.Windows.Forms;

namespace MobileShopUI
{
    public class MainForm : Form
    {
        private MenuStrip menuStrip=new MenuStrip();
        private ToolStripMenuItem menuSanPham=new ToolStripMenuItem();
        private ToolStripMenuItem menuKhachHang=new ToolStripMenuItem();
        private ToolStripMenuItem menuDonHang=new ToolStripMenuItem();
        private ToolStripMenuItem thongkeDonHang=new ToolStripMenuItem();

        public MainForm()
        {
            this.Text = "Trang Chính";
            this.Width = 800;
            this.Height = 600;

            InitializeMenu();
        }

        private void InitializeMenu()
        {
            // Tạo MenuStrip
            menuStrip = new MenuStrip();

            // Danh mục Sản phẩm
            menuSanPham = new ToolStripMenuItem("Danh mục sản phẩm");
            menuSanPham.Click += (s, e) =>
            {
                MessageBox.Show("Chức năng Danh mục sản phẩm đang được xây dựng!");
            };
            menuStrip.Items.Add(menuSanPham);

            // Danh mục Khách hàng
            menuKhachHang = new ToolStripMenuItem("Danh mục khách hàng");
            menuKhachHang.Click += (s, e) =>
            {
                MessageBox.Show("Chức năng Danh mục khách hàng đang được xây dựng!");
            };
            menuStrip.Items.Add(menuKhachHang);

            // Thống kê doanh thu
            thongkeDonHang =new ToolStripMenuItem("Thống kê doanh thu");
            thongkeDonHang.Click +=(s,e) =>{
                ThongKeForm thongKeForm = new ThongKeForm();
                thongKeForm.Show();
            };
            menuStrip.Items.Add(thongkeDonHang);
            // Đơn hàng
            menuDonHang = new ToolStripMenuItem("Đơn hàng");
            menuDonHang.Click += (s, e) =>
            {
                DonHangForm donHangForm = new DonHangForm();
                donHangForm.Show();
            };
            menuStrip.Items.Add(menuDonHang);

            // Thêm MenuStrip vào Form
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }
    }
}
