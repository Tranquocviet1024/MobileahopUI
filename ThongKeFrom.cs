using System;
using System.Data;
using System.Windows.Forms;

namespace MobileShopUI
{
    public class ThongKeForm : Form
    {
        private ComboBox cmbThang = new ComboBox();
        private TextBox txtNam = new TextBox();
        private Button btnThongKe = new Button();
        private Label lblTongDoanhThu = new Label();
        private TextBox txtTongDoanhThu = new TextBox();
        private DataGridView dgvChiTietDoanhThu = new DataGridView();

        public ThongKeForm()
        {
            this.Text = "Thống Kê Doanh Thu";
            this.Width = 800;
            this.Height = 600;

            InitializeControls();
        }

        private void InitializeControls()
        {
            // ComboBox chọn tháng
            Label lblThang = new Label { Text = "Tháng:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.Controls.Add(lblThang);

            cmbThang.Location = new System.Drawing.Point(100, 20);
            cmbThang.Width = 100;
            for (int i = 1; i <= 12; i++)
            {
                cmbThang.Items.Add(i);
            }
            this.Controls.Add(cmbThang);

            // TextBox chọn năm
            Label lblNam = new Label { Text = "Năm:", Location = new System.Drawing.Point(250, 20), AutoSize = true };
            this.Controls.Add(lblNam);

            txtNam.Location = new System.Drawing.Point(300, 20);
            txtNam.Width = 100;
            this.Controls.Add(txtNam);

            // Button Thống Kê
            btnThongKe = new Button
            {
                Text = "Thống Kê",
                Location = new System.Drawing.Point(450, 20),
                Width = 100
            };
            btnThongKe.Click += BtnThongKe_Click;
            this.Controls.Add(btnThongKe);

            // Label hiển thị tổng doanh thu
            lblTongDoanhThu = new Label
            {
                Text = "Tổng Doanh Thu:",
                Location = new System.Drawing.Point(20, 70),
                AutoSize = true
            };
            this.Controls.Add(lblTongDoanhThu);

            txtTongDoanhThu = new TextBox
            {
                Location = new System.Drawing.Point(150, 70),
                Width = 200,
                ReadOnly = true
            };
            this.Controls.Add(txtTongDoanhThu);

            // DataGridView hiển thị chi tiết doanh thu
            dgvChiTietDoanhThu = new DataGridView
            {
                Location = new System.Drawing.Point(20, 120),
                Width = 750,
                Height = 400,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            this.Controls.Add(dgvChiTietDoanhThu);
        }

        private void BtnThongKe_Click(object? sender, EventArgs e)
        {
            try
            {
                // Lấy tháng và năm từ ComboBox và TextBox
                if (cmbThang.SelectedItem == null || string.IsNullOrEmpty(txtNam.Text))
                {
                    MessageBox.Show("Vui lòng chọn tháng và nhập năm!");
                    return;
                }

                int thang = Convert.ToInt32(cmbThang.SelectedItem);
                int nam = Convert.ToInt32(txtNam.Text);

                // Kết nối cơ sở dữ liệu và thực hiện truy vấn
                KetNoi db = new KetNoi();
                string query = @"
                    SELECT 
                        SUM(soluong * sanpham.gia) AS TongDoanhThu
                    FROM DonHang
                    INNER JOIN SanPham ON DonHang.sanphamid = SanPham.id
                    WHERE MONTH(ngaymua) = @thang AND YEAR(ngaymua) = @nam";

                var parameters = new Dictionary<string, object>
                {
                    { "@thang", thang },
                    { "@nam", nam }
                };

                var ds = db.LayDuLieuCoThamSo(query, parameters);

                // Hiển thị tổng doanh thu
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    object doanhThu = ds.Tables[0].Rows[0]["TongDoanhThu"];
                    txtTongDoanhThu.Text = doanhThu != DBNull.Value ? doanhThu.ToString() : "0";
                }

                // Hiển thị chi tiết doanh thu (nếu cần)
                string queryChiTiet = @"
                    SELECT 
                        DonHang.id AS 'Mã Đơn Hàng',
                        SanPham.tenSP AS 'Tên Sản Phẩm',
                        DonHang.soluong AS 'Số Lượng',
                        SanPham.gia AS 'Đơn Giá',
                        (DonHang.soluong * SanPham.gia) AS 'Thành Tiền',
                        DonHang.ngaymua AS 'Ngày Mua'
                    FROM DonHang
                    INNER JOIN SanPham ON DonHang.sanphamid = SanPham.id
                    WHERE MONTH(DonHang.ngaymua) = @thang AND YEAR(DonHang.ngaymua) = @nam";

                var dsChiTiet = db.LayDuLieuCoThamSo(queryChiTiet, parameters);
                if (dsChiTiet != null && dsChiTiet.Tables.Count > 0)
                {
                    dgvChiTietDoanhThu.DataSource = dsChiTiet.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
