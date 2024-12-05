using QuanLiNhanVien.Database;
using QuanLiNhanVien.Service;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private AppDbContext _context;
        private RegisterService _registerService;
        public Register()
        {
            InitializeComponent();
            _registerService = new RegisterService();
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = UsernameTextBox.Text;
            var passWord = PasswordBox.Password;
            var fullName = FullNameTextBox.Text;
            var gender = GenderBox.Text;
            var address = AddressBox.Text;
            var phone = PhoneBox.Text;
            //Đăng kí user
            var user = _registerService.RegisterEmployee(userName, passWord, fullName, gender, address, phone);
            if (user != null)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Existed Username or password.", "Register Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Có tài khoản rồi thì chuyển sang đăng nhập
        public void Register_Move_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();

        }
    }
}
