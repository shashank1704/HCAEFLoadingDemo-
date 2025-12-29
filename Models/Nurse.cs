namespace HCAEFLoadingDemo.Models
{
    public class Nurse
    {
        public int NurseID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();  // Virtual for lazy
    }
}

