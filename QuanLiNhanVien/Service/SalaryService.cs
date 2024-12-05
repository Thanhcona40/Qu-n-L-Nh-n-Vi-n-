using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiNhanVien.Service
{
    class SalaryService
    {
        private AppDbContext _appDbContext;
        
        public SalaryService()
        {
            _appDbContext = new AppDbContext();
        }

        public void AddSalary(Salary salary)
        {
            if (salary != null)
            {
                _appDbContext.Salary.Add(salary);
                _appDbContext.SaveChanges();
            }
            else
            {
                MessageBox.Show("Invalid value");
            }
        }

        public List<Salary> GetSalaryList()
        {
            return _appDbContext.Salary.Include(u => u.Employee).ToList();
        }

        public List<Salary> GetFilterDataSalaryList(int? employeeId, int? month, int? year)
        {
            // Khởi tạo truy vấn ban đầu
            var query = _appDbContext.Salary.AsQueryable();

            // Lọc theo EmployeeId nếu không phải null
            if (employeeId.HasValue)
            {
                query = query.Where(s => s.EmployeeId == employeeId.Value);
            }

            // Lọc theo Month nếu không phải null
            if (month.HasValue)
            {
                query = query.Where(s => s.Month == month.Value);
            }

            // Lọc theo Year nếu không phải null
            if (year.HasValue)
            {
                query = query.Where(s => s.Year == year.Value);
            }

            // Trả về danh sách kết quả sau khi lọc
            return query.ToList();
        }

    }
}
