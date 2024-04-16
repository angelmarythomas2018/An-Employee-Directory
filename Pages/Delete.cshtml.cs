using EmployeeMangement.Models;
using EmployeeMangement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeMangement.Pages
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDBContext _context;

        public DeleteModel(ApplicationDBContext context)
        {
            _context = context;
        }
        //public void OnGet()
        //{
        //}
        [BindProperty]
        public Employees Employee { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FirstOrDefaultAsync(p => p.EmpID == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            _context.Employee.Remove(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin");
        }
           
        public async Task<IActionResult> OnPostAsync( string id)
        {
            if ((id== null))
            {
                return NotFound();
            }
            var employee = await _context.Employee.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmpID == Employee.EmpID);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            _context.Employee.Remove(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin");

        }

        
    }
}
