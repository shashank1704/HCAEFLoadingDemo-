namespace HCAEFLoadingDemo.Models
{
    public class Shift
    {
        public int ShiftID { get; set; }
        public int NurseID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal DurationHours { get; set; }
        public string? Notes { get; set; }

        public virtual Nurse Nurse { get; set; } = null!;  // Virtual for lazy
    }
}

