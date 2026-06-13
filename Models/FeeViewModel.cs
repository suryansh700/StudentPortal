namespace StudentPortal.Models
{
    public class FeeViewModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = "";

        public string RollNo { get; set; } = "";

        public string PhotoPath { get; set; } = "";

        public decimal TotalFee { get; set; }

        public decimal PaidFee { get; set; }

        public decimal DueFee { get; set; }

        public string Status { get; set; } = "";
    }
}