using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeMangement.Pages
{
    public class EditModel : PageModel
    {
        
        private readonly ApplicationDBContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ApplicationDBContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Employees Employee { get; set; }

        public List<SelectListItem> DepartmentOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Employee==null)
            {
                return NotFound();
            }
            //    var employee = _context.Employee.FirstOrDefaultAsync(p => p.EmpID == id);
            //   // Employee = await _context.Employee.FindAsync(id);

            //    if (employee == null)
            //    {
            //        return NotFound();
            //    }

            //    // Fetch department names from the database
            //    var departments = await _context.Department.ToListAsync();

            //    // Map department names to SelectListItem objects
            //    DepartmentOptions = departments
            //        .Select(d => new SelectListItem { Value = d.DepartmentName, Text = d.DepartmentName })
            //        .ToList();
            //    Employee = employee;
            //    return Page();
           
            Employee = await _context.Employee
               .Include(e => e.Department) // Ensure the department is also loaded if needed
               .FirstOrDefaultAsync(e => e.EmpID == id);

            if (Employee == null)
            {
                return NotFound();
            }

            // Fetch department names from the database
            try
            {
                var departments = await _context.Department.ToListAsync();
                // Map department names to SelectListItem objects
                DepartmentOptions = departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentName,
                    Text = d.DepartmentName
                }).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your error handling policy
                // Optionally, set DepartmentOptions to an empty list or handle the error appropriately
                DepartmentOptions = new List<SelectListItem>();
            }

            return Page();
        }

    

    public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        _logger.LogError($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }

                return Page();
            }
            
            var dbEmployee = await _context.Employee.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmpID == Employee.EmpID);
            if (dbEmployee == null)
            {
                ModelState.AddModelError("", "Employee Not Found");
                return Page();
            }

            // Update only the fields that should be updated
            dbEmployee.EmpName = Employee.EmpName;
            dbEmployee.EmpEmail = Employee.EmpEmail;
            dbEmployee.EmpPhoneNumber = Employee.EmpPhoneNumber;
            dbEmployee.Department.DepartmentName = Employee.Department.DepartmentName; // Assuming you have a DepartmentId


            //_context.Employee.Update(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin");
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.EmpID == id);
        }
    }
}
