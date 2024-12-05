using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Database;
using QuanLiNhanVien.Service;
using QuanLiNhanVien.SessionUser;
using QuanLiNhanVien.View.Admin;
using QuanLiNhanVien.View.User;
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
using System.Windows.Shapes;

namespace QuanLiNhanVien.View.Authentication
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginService _loginService;
        public Login()
        {
            InitializeComponent();
            _loginService = new LoginService();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password.ToString();

            if (!string.IsNullOrWhiteSpace(username) || !string.IsNullOrWhiteSpace(password))
            {
                var user = _loginService.LoginEmployee(username, password);

                if (user != null)
                {
                    // Nếu tìm thấy người dùng, lưu vào UserSession
                    EmployeeSession.Instance.SetEmployee(user);
                    if (EmployeeSession.Instance.Role == "ADMIN")
                    {
                        // Mở cửa sổ dành cho admin
                        NavBarAdmin adminWindow = new NavBarAdmin();
                        adminWindow.Show();
                        this.Close();
                    }
                    else if (EmployeeSession.Instance.Role == "USER")
                    {
                        // Mở cửa sổ dành cho user
                        NavBarUser userWindow = new NavBarUser();
                        userWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid text box.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Nếu chưa có tài khoản thì sang trang đăng kí
        public void Login_Move_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

    }
}
