using QuanLiNhanVien.Model;
using QuanLiNhanVien.Service;
using QuanLiNhanVien.SessionUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiNhanVien.View.User
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        private EmployeeService _employeeService;
        public Profile()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            LoadEmployee();
        }

        public void LoadEmployee()
        {
            int employeeId = EmployeeSession.Instance.Id;
            Employee employee = _employeeService.GetEmployee(employeeId);

            if (employee != null)
            {
                EmployeeProfileGrid.DataContext = employee;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin nhân viên từ DataContext (nếu có)
            var employee = (Employee)EmployeeProfileGrid.DataContext;

            if (employee != null)
            {
                var fullNameTextBox = (TextBox)EmployeeProfileGrid.FindName("FullName");
                var genderTextBox = (TextBox)EmployeeProfileGrid.FindName("Gender");
                var addressTextBox = (TextBox)EmployeeProfileGrid.FindName("Address");
                var phoneTextBox = (TextBox)EmployeeProfileGrid.FindName("Phone");

                // Kiểm tra xem các TextBox có null không trước khi gán giá trị
                if (fullNameTextBox != null) employee.FullName = fullNameTextBox.Text;
                if (genderTextBox != null) employee.Gender = genderTextBox.Text;
                if (addressTextBox != null) employee.Address = addressTextBox.Text;
                if (phoneTextBox != null) employee.Phone = phoneTextBox.Text;
                bool isUpdated = _employeeService.UpdateEmployee(employee);
                if (isUpdated)
                {
                    MessageBox.Show("Thông tin đã được cập nhật thành công.");
                    LoadEmployee();  // Đảm bảo LoadEmployee được gọi để làm mới giao diện
                }
                else
                {
                    MessageBox.Show("Đã có lỗi khi cập nhật thông tin.");
                }
                LoadEmployee();
            }
        }
    }
}
