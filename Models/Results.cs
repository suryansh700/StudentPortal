namespace StudentPortal.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = "";

        public string RollNo { get; set; } = "";

        public int Java { get; set; }

        public int Python { get; set; }

        public int DBMS { get; set; }

        public double Percentage { get; set; }

        public string Grade { get; set; } = "";
    }
}