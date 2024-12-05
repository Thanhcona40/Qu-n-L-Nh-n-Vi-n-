
using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLiNhanVien.SessionUser
{
    public class EmployeeSession
    {
        private static EmployeeSession _instance;

        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } 

        // Constructor riêng tư để đảm bảo không thể tạo mới instance từ bên ngoài
        private EmployeeSession() { }

        // Singleton pattern - chỉ tạo một instance của UserSession
        public static EmployeeSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeSession();
                }
                return _instance;
            }
        }

        public void SetEmployee(Model.Employee employee)
        {
            Id = employee.Id;
            Username = employee.Username;
            FullName = employee.FullName;
            Gender = employee.Gender;
            Address = employee.Address;
            Phone = employee.Phone;
            Role = employee.Role;
            
        }
        public void ClearSession()
        {
            Username = null;
            Role = null;
            Gender = null;
            Address = null;
            Phone = null;
        }
    }

}
