using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models
{
    public class OrderViewModel
    {
        [Key]
        public string? IdOrder { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? Address { get; set; }

        public int SLDat { get; set; }
        public decimal TongTien { get; set; }


        public DateTime NgayDatHang { get; set; }
    }
}
