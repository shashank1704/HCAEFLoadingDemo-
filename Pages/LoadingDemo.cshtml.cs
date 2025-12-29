using HCAEFLoadingDemo.Data;
using HCAEFLoadingDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HCAEFLoadingDemo.Pages
{
    public class LoadingDemoModel : PageModel
    {
        private readonly HCADbContext _context;

        public LoadingDemoModel(HCADbContext context)
        {
            _context = context;
        }

        public Nurse? Nurse { get; set; }
        public string LoadingStrategy { get; set; } = string.Empty;
        public List<string> QueriesExecuted { get; set; } = new();

        public async Task OnGetAsync(int nurseId = 1, string strategy = "lazy")
        {
            LoadingStrategy = strategy.ToUpper();
            QueriesExecuted.Add("Main query: SELECT * FROM Nurses WHERE NurseID = " + nurseId);

            if (strategy == "lazy")
            {
                Nurse = await _context.Nurses.FirstOrDefaultAsync(n => n.NurseID == nurseId);
                if (Nurse != null)
                {
                    var shifts = Nurse.Shifts.ToList();  // Triggers lazy load
                    QueriesExecuted.Add("Lazy query: SELECT * FROM Shifts WHERE NurseID = " + nurseId);
                }
            }
            else if (strategy == "eager")
            {
                Nurse = await _context.Nurses
                    .Include(n => n.Shifts)
                    .FirstOrDefaultAsync(n => n.NurseID == nurseId);
                QueriesExecuted.Add("Eager query: SELECT * FROM Nurses LEFT JOIN Shifts ON ...");
            }
            else if (strategy == "explicit")
            {
                Nurse = await _context.Nurses.FirstOrDefaultAsync(n => n.NurseID == nurseId);
                if (Nurse != null)
                {
                    await _context.Entry(Nurse).Collection(n => n.Shifts).LoadAsync();  // Explicit load
                    QueriesExecuted.Add("Explicit query: SELECT * FROM Shifts WHERE NurseID = " + nurseId);
                }
            }
        }
    }
}
