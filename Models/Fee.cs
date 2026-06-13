using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPortal.Models
{
    public class Fee
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = "";

        public string RollNo { get; set; } = "";

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DueFee { get; set; }

        public string Status { get; set; } = "";
    }
}