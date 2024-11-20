namespace MobileShopUI
{
    public class NguoiDung
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; } = string.Empty; // Khởi tạo giá trị mặc định
        public string MatKhau { get; set; } = string.Empty;


        public NguoiDung() { }

        public NguoiDung(int id, string tenDangNhap, string matKhau)
        {
            Id = id;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
        }
    }

}