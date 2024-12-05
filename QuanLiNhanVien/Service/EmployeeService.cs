using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using QuanLiNhanVien.SessionUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Service
{
    class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService()
        {
            _context = new AppDbContext();
        }

        public DataTable GetAllEmployees(string role)
        {
            var employees = _context.Employees.Where(r => r.Role == role).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Role", typeof(string));

            foreach (var employee in employees)
            {
                DataRow row = dt.NewRow();
                row["ID"] = employee.Id;
                row["Username"] = employee.Username;
                row["FullName"] = employee.FullName;
                row["Gender"] = employee.Gender;
                row["Address"] = employee.Address;
                row["Phone"] = employee.Phone;
                row["Role"] = employee.Role;

                dt.Rows.Add(row);
            }

            return dt;
        }

        // Hàm thêm mới một nhân viên
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == employeeId);
        }

        // Hàm cập nhật thông tin nhân viên
        public bool UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.Id);
            try
            {
                existingEmployee.FullName = employee.FullName;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Address = employee.Address;
                existingEmployee.Phone = employee.Phone;
                EmployeeSession.Instance.SetEmployee(existingEmployee);
                _context.Employees.Update(existingEmployee);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //Hàm lấy ra danh sách id nhân viên
        public List<Employee> GetEmployees()
        {
            return _context.Employees.Where(u => u.Role == "USER").ToList();
        }

        // Hàm xóa nhân viên
        public void DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
