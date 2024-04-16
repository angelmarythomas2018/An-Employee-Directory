using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangement.Pages
{
    public class AddEmployeeModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public AddEmployeeModel(ApplicationDBContext context)
        {
            _context = context;
        }

      
        [BindProperty]
        public Employees Employee { get; set; }
        public List<SelectListItem> DepartmentOptions { get; set; }
        public async Task OnGetAsync()
        {
            // Fetch department names from the database
            var departments = await _context.Department.ToListAsync();

            // Map department names to SelectListItem objects
            DepartmentOptions = departments
                .Select(d => new SelectListItem { Value = d.DepartmentName, Text = d.DepartmentName })
                .ToList();
        }
        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid || _context.Employee==null || Employee==null) 
            {
            return Page();
            }
            //var selectedDepartment = await _context.Department.FindAsync(Employee.DepartmentId);
            //if (selectedDepartment == null)
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid department selected.");
            //    return Page();
            //}
            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin");
        }
    }
}
