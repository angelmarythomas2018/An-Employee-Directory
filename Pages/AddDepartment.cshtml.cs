using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMangement.Pages
{
    public class AddDepartmentModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public AddDepartmentModel(ApplicationDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Departments Department { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Normalize the input to uppercase (or lowercase)
            string normalizedInputName = Department.DepartmentName.ToUpper();

            // Check if the department name already exists in the database
            bool departmentExists = await _context.Department
                .AnyAsync(d => d.DepartmentName.ToUpper() == normalizedInputName);

            if (departmentExists)
            {
                // Add a model error on the DepartmentName field to indicate the name is already taken
                ModelState.AddModelError("Department.DepartmentName", "Department name already exists. Please use a different name.");

                return Page(); // Return the page with validation error
            }

            _context.Department.Add(Department);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin");
        }
    }
}
