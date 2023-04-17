namespace codeBTL.Models
{
    public class HistoryOrderAPI
    {
        public string? MaDh { get; set; }

        public string? MaSp { get; set; }

        public string? TenSp { get; set; }

        public string? AnhDaiDien { get; set; } = null!;

        public DateTime? NgayDat { get; set; }
        public int? Sldat { get; set; }
        public decimal? TongTien { get; set; }
    }
}
