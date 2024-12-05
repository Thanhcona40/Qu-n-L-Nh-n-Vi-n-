using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Service
{
    class AttendanceService
    {
        private readonly AppDbContext _context;

        public AttendanceService() 
        { 
            _context = new AppDbContext();
        }

        public void AddAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
        }

        //Lấy lịch làm việc của một nhân viên dựa theo tháng 
        public List<Model.Attendance> GetAttendanceByMonth(int employeeId, int year, int month)
        {
            return _context.Attendances
                .Where(ws => ws.EmployeeId == employeeId
                          && ws.dateAttendance.Year == year
                          && ws.dateAttendance.Month == month)
                .ToList();
        }
    }
}
