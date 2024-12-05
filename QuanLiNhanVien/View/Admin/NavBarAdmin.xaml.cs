using QuanLiNhanVien.SessionUser;
using QuanLiNhanVien.View.Authentication;
using QuanLiNhanVien.View.Notification;
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

namespace QuanLiNhanVien.View.Admin
{
    /// <summary>
    /// Interaction logic for NavBarAdmin.xaml
    /// </summary>
    public partial class NavBarAdmin : Window
    {
        public NavBarAdmin()
        {
            InitializeComponent();
        }
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            // Tạm thời chưa hiển thị nội dung gì cho Dashboard
            MainContent.Content = null;
        }

        private void ManageEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageEmployee();
        }

        private void ManageWorkSchedule_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageWorkSchedule();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Notifications();
        }

        private void ManageSalary_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ManageSalary();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //UserSession.Instance.ClearSession();
            Login login = new Login();
            login.Show();
            this.Close();
        }

    }
}
