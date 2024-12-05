using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Service
{
    public class RegisterService
    {
        private readonly AppDbContext _context;
        private WorkScheduleService _workScheduleService;

        public RegisterService()
        {
            _context = new AppDbContext();
            _workScheduleService = new WorkScheduleService();
        }

        public Employee RegisterEmployee(string username, string password, string fullName, string gender, string address,
            string phone)
        {

            // Tạo đối tượng User mới
            var employee = new Employee
            {
                Username = username,
                Password = password,
                FullName = fullName,
                Gender = gender,
                Address = address,
                Phone = phone,
                Role = "USER", // Mặc định là User

            };

            // Lưu vào cơ sở dữ liệu
            _context.Employees.Add(employee);
            _context.SaveChanges();

            int employeeId = employee.Id;  

            // Tự động phân lịch cho nhân viên mới
            _workScheduleService.AutoAssignShifts(employeeId);

            return employee;
        }
    }
}
