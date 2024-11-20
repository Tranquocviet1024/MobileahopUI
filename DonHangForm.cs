using System;
using System.Data;
using System.Windows.Forms;

namespace MobileShopUI
{
    public class DonHangForm : Form
    {
        private DataGridView dgvDonHang = new DataGridView();
        private TextBox txtId = new TextBox();
        private TextBox txtKhachHangId = new TextBox();
        private TextBox txtSanPhamId = new TextBox();
        private TextBox txtSoLuong = new TextBox();
        private DateTimePicker dtpNgayMua = new DateTimePicker();
        private Button btnThem = new Button();
        private Button btnSua = new Button();
        private Button btnXoa = new Button();
        private Button btnTimKiem = new Button();
        private TextBox txtTimKiem = new TextBox();

        public DonHangForm()
        {
            this.Text = "Quản lý Đơn hàng";
            this.Width = 900;
            this.Height = 700;

            InitializeControls();
        }

        private void InitializeControls()
        {
            // DataGridView
            dgvDonHang = new DataGridView
            {
                Location = new System.Drawing.Point(20, 20),
                Width = 850,
                Height = 300,
                AllowUserToAddRows = false
            };
            LoadDonHang(); // Load dữ liệu lên DataGridView
            this.Controls.Add(dgvDonHang);

            // Labels và TextBox để nhập thông tin
            Label lblId = new Label { Text = "ID:", Location = new System.Drawing.Point(20, 350), AutoSize = true };
            this.Controls.Add(lblId);
            txtId.Location = new System.Drawing.Point(120, 350);
            this.Controls.Add(txtId);

            Label lblKhachHangId = new Label { Text = "Khách hàng ID:", Location = new System.Drawing.Point(20, 390), AutoSize = true };
            this.Controls.Add(lblKhachHangId);
            txtKhachHangId.Location = new System.Drawing.Point(120, 390);
            this.Controls.Add(txtKhachHangId);

            Label lblSanPhamId = new Label { Text = "Sản phẩm ID:", Location = new System.Drawing.Point(20, 430), AutoSize = true };
            this.Controls.Add(lblSanPhamId);
            txtSanPhamId.Location = new System.Drawing.Point(120, 430);
            this.Controls.Add(txtSanPhamId);

            Label lblSoLuong = new Label { Text = "Số lượng:", Location = new System.Drawing.Point(20, 470), AutoSize = true };
            this.Controls.Add(lblSoLuong);
            txtSoLuong.Location = new System.Drawing.Point(120, 470);
            this.Controls.Add(txtSoLuong);

            Label lblNgayMua = new Label { Text = "Ngày mua:", Location = new System.Drawing.Point(20, 510), AutoSize = true };
            this.Controls.Add(lblNgayMua);
            dtpNgayMua.Location = new System.Drawing.Point(120, 510);
            this.Controls.Add(dtpNgayMua);

            // Button Thêm
            btnThem = new Button
            {
                Text = "Thêm",
                Location = new System.Drawing.Point(20, 560)
            };
            btnThem.Click += BtnThem_Click;
            this.Controls.Add(btnThem);

            // Button Sửa
            btnSua = new Button
            {
                Text = "Sửa",
                Location = new System.Drawing.Point(120, 560)
            };
            btnSua.Click += BtnSua_Click;
            this.Controls.Add(btnSua);

            // Button Xóa
            btnXoa = new Button
            {
                Text = "Xóa",
                Location = new System.Drawing.Point(220, 560)
            };
            btnXoa.Click += BtnXoa_Click;
            this.Controls.Add(btnXoa);

            // TextBox Tìm kiếm
            txtTimKiem = new TextBox
            {
                Location = new System.Drawing.Point(600, 350),
                Width = 200
            };
            this.Controls.Add(txtTimKiem);

            // Button Tìm kiếm
            btnTimKiem = new Button
            {
                Text = "Tìm kiếm",
                Location = new System.Drawing.Point(820, 350)
            };
            btnTimKiem.Click += BtnTimKiem_Click;
            this.Controls.Add(btnTimKiem);
        }

        private void LoadDonHang()
        {
            // Kết nối cơ sở dữ liệu và load danh sách đơn hàng
            KetNoi db = new KetNoi();
            string query = "SELECT * FROM DonHang";
            var ds = db.LayDuLieu(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                dgvDonHang.DataSource = ds.Tables[0];
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string khachHangId = txtKhachHangId.Text.Trim();
            string sanPhamId = txtSanPhamId.Text.Trim();
            string soLuong = txtSoLuong.Text.Trim();
            string ngayMua = dtpNgayMua.Value.ToString("yyyy-MM-dd");

            // Kiểm tra dữ liệu nhập
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(khachHangId) || string.IsNullOrEmpty(sanPhamId) || string.IsNullOrEmpty(soLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Thêm vào cơ sở dữ liệu
            KetNoi db = new KetNoi();
            string query = $"INSERT INTO DonHang (id, khachhangid, sanphamid, soluong, ngaymua) VALUES ({id}, {khachHangId}, {sanPhamId}, {soLuong}, '{ngayMua}')";

            if (db.ThucThi(query))
            {
                MessageBox.Show("Thêm đơn hàng thành công!");
                LoadDonHang(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show("Thêm đơn hàng thất bại!");
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Sửa đang được xây dựng!");
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                string? donHangId = dgvDonHang.CurrentRow.Cells["id"]?.Value?.ToString();
                KetNoi db = new KetNoi();
                string query = $"DELETE FROM DonHang WHERE id = {donHangId}";

                if (db.ThucThi(query))
                {
                    MessageBox.Show("Xóa đơn hàng thành công!");
                    LoadDonHang();
                }
                else
                {
                    MessageBox.Show("Xóa đơn hàng thất bại!");
                }
            }
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            KetNoi db = new KetNoi();
            string query = $"SELECT * FROM DonHang WHERE khachhangid LIKE '%{keyword}%'";

            var ds = db.LayDuLieu(query);

            if (ds != null && ds.Tables.Count > 0)
            {
                dgvDonHang.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Không tìm thấy kết quả!");
            }
        }
    }
}
