using System.ComponentModel.DataAnnotations;

namespace EmployeeMangement.Models
{
    public class Employees
    {
        [Key]
        [Required]
        public string EmpID { get; set; }

        [Required]
        public string EmpName { get; set; }
        public string EmpPhoneNumber { get; set; }
        public string EmpEmail { get; set; }
        [Required]
        public int DepartmentId { get; set; }

        public Departments Department { get; set; }

        public static implicit operator Employees(Task<Employees?> v)
        {
            throw new NotImplementedException();
        }
    }
}
