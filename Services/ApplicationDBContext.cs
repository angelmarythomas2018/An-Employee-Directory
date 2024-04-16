using EmployeeMangement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeMangement.Services
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employees> Employee { get; set; }
        public DbSet<Departments> Department { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var data_entry_operator = new IdentityRole("data_entry_operator");
            data_entry_operator.NormalizedName = "data_entry_operator";

            var employee = new IdentityRole("employee");
            employee.NormalizedName = "employee";

            builder.Entity<IdentityRole>().HasData(admin, data_entry_operator,employee);
        }
    }
}
