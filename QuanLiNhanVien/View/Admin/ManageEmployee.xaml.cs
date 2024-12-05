using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using QuanLiNhanVien.Service;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace QuanLiNhanVien.View.Admin
{
    /// <summary>
    /// Interaction logic for AddDepartment.xaml
    /// </summary>
    public partial class ManageEmployee : UserControl
    {
        private EmployeeService _employeeService;
        public ManageEmployee()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            LoadEmployees();
        }

        public void LoadEmployees()
        {
            string role = "USER";
            DataTable employees = _employeeService.GetAllEmployees(role);
            EmployeeGrid.ItemsSource = employees.DefaultView;
        }
        //Xóa nhân viên 
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int employeeId = (int)deleteButton.Tag;

            // Thực hiện việc xóa Employee
            var result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _employeeService.DeleteEmployee(employeeId);
                LoadEmployees(); // Tải lại danh sách nhân viên sau khi xóa
            }
        }
    }
}
