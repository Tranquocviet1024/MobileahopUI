namespace MobileShopUI
{
    public class DonHang
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayMua { get; set; }

        public DonHang() { }

        public DonHang( int id,int khachHangId, int sanPhamId, int soLuong, DateTime ngayMua)
        {
            Id=id;
            KhachHangId = khachHangId;
            SanPhamId = sanPhamId;
            SoLuong = soLuong;
            NgayMua = ngayMua;
        }
    }

}