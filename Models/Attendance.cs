namespace StudentPortal.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = "";

        public string RollNo { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime Date { get; set; }
    }
}