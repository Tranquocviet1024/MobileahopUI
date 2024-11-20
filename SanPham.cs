namespace MobileShopUI
{
    public class SanPham
    {
        public int Id { get; set; }
        public string TenSP { get; set; } =String.Empty;
        public double Gia { get; set; }
        public string HangSX { get; set; }=String.Empty;

        public SanPham() { }

        public SanPham(int id, string tenSP, double gia, string hangSX)
        {
            Id = id;
            TenSP = tenSP;
            Gia = gia;
            HangSX = hangSX;
        }
    }

}