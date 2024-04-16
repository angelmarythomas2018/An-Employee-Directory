using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangement.Pages
{
    [Authorize(Roles ="data_entry_operator")]
    public class DataEntryOperatorModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public DataEntryOperatorModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<Employees> Employee { get; set; }
        public List<Departments> Department { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Employee != null)
            {
                Employee = await _context.Employee.Include(e => e.Department).ToListAsync();
            }
        }
    }
}
