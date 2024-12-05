using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhanVien.Database
{
        public class AppDbContext : DbContext
        {
            public DbSet<Employee> Employees { get; set; }

            public DbSet<Salary> Salary {  get; set; }

            public DbSet<WorkSchedule> WorkSchedules { get; set; }

            public DbSet<Notification> Nofications { get; set; }

            public DbSet<Attendance> Attendances { get; set; }

            
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-L5HBD2L9\MSSQLSERVERA40;Database=QLNV;Trusted_Connection=True;TrustServerCertificate=True;");
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập quan hệ 1-n giữa User và Salary, WorkSchedule, Notification, Attendance
            modelBuilder.Entity<Salary>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(ws => ws.EmployeeId);

            modelBuilder.Entity<WorkSchedule>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(ws => ws.EmployeeId);

            modelBuilder.Entity<Notification>()
                .HasOne(u => u.Employee)
                .WithMany()
                .HasForeignKey(ws => ws.EmployeeId);
        }
    }
}
