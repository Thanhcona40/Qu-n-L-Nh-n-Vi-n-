using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Model
{
    public class Attendance
    {
        public int Id {  get; set; }
        public DateTime dateAttendance {  get; set; }

        public int EmployeeId { get; set; }

        public Employee employee { get; set; }
    }
}
