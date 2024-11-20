using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MobileShopUI
{
    public class LoginForm : Form
    {
        private Label lblUsername = new Label();
        private Label lblPassword = new Label();
        private TextBox txtUsername = new TextBox();
        private TextBox txtPassword = new TextBox();
        private Button btnLogin = new Button();

        public LoginForm()
        {
            this.Text = "Đăng Nhập";
            this.Width = 400;
            this.Height = 250;

            InitializeControls();
        }

        private void InitializeControls()
        {
            lblUsername = new Label
            {
                Text = "Tên đăng nhập:",
                Location = new System.Drawing.Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblUsername);

            txtUsername = new TextBox
            {
                Location = new System.Drawing.Point(150, 25),
                Width = 200
            };
            this.Controls.Add(txtUsername);

            lblPassword = new Label
            {
                Text = "Mật khẩu:",
                Location = new System.Drawing.Point(30, 80),
                AutoSize = true
            };
            this.Controls.Add(lblPassword);

            txtPassword = new TextBox
            {
                Location = new System.Drawing.Point(150, 75),
                Width = 200,
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtPassword);

            btnLogin = new Button
            {
                Text = "Đăng Nhập",
                Location = new System.Drawing.Point(150, 130),
                Width = 100
            };
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Đăng nhập thành công!");
                MainForm mainForm = new MainForm();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            KetNoi db = new KetNoi();
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE LOWER(tendangnhap) = LOWER(@username) AND matkhau = @password";

            try
            {
                Console.WriteLine($"DEBUG - Username: {username}, Password: {password}");

                var parameters = new Dictionary<string, object>
        {
            { "@username", username },
            { "@password", password }
        };

                var ds = db.LayDuLieuCoThamSo(query, parameters);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    return result > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
                return false;
            }
        }
        
    }
}
