using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Service
{
    class NotificationService
    {
        private AppDbContext _appDbContext;

        public NotificationService() 
        {
            _appDbContext = new AppDbContext();
        }

        public List<Notification> GetAllNotifications()
        {
            return _appDbContext.Nofications
                .Include(e => e.Employee)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        // Thêm thông báo mới vào cơ sở dữ liệu
        public void AddNotification(string title, string content, int employeeId)
        {
            var newNotification = new Notification
            {
                Title = title,
                Content = content,
                CreatedAt = DateTime.Now,
                EmployeeId = employeeId
            };

            _appDbContext.Nofications.Add(newNotification);
            _appDbContext.SaveChanges();
        }
    }
}
