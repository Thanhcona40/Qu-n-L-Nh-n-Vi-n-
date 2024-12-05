using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Model
{
    public class Salary
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int TotalHoursWorked { get; set; }

        public int TotalSalary { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
