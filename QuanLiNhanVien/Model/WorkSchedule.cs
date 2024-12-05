using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Model
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public DateTime WorkDate { get; set; }
       
        public string Shift {  get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
