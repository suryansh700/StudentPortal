namespace StudentPortal.Models
{
    public class AttendanceViewModel
    {
        public int StudentId { get; set; }

        public string Name { get; set; } = "";

        public string RollNo { get; set; } = "";

        public string PhotoPath { get; set; } = "";

        public string Status { get; set; } = "Present";
    }
}