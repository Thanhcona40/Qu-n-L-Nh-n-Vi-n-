using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using QuanLiNhanVien.SessionUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiNhanVien.Service
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService()
        {
            _context = new AppDbContext();
        }

        public Employee LoginEmployee(string username, string password)
        {
            //Tìm User dựa trên username và password
           var user = _context.Employees.FirstOrDefault(u => u.Username == username && u.Password == password);

            // Kiểm tra xem User có tồn tại không
            if (user == null)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }

            return user;
        }

    }
}
