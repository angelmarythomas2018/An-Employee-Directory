using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangement.Pages
{
    [Authorize(Roles ="admin")]
    public class AdminModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public AdminModel(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<Employees> Employee{ get; set; }
        public List<Departments> Department{ get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Employee != null)
            {
                Employee = await _context.Employee.Include(e => e.Department).ToListAsync();
            }
        }
        //public async Task OnGetAsync(string searchTerm)
        //{
        //    IQueryable<Employees> query = _context.Employee.Include(e => e.Department);

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query = query.Where(e => e.EmpName.Contains(searchTerm));
        //    }

        //    Employee = await query.ToListAsync();
        //}
        //public async Task OnGetAsync(string searchTerm)
        //{
        //    IQueryable<Employees> query = _context.Employee.Include(e => e.Department);

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query = query.Where(e => e.EmpID.Contains(searchTerm));
        //    }

        //    Employee = await query.ToListAsync();
        //}

    }
}
