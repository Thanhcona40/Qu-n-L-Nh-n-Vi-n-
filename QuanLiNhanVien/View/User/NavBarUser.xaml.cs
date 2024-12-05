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

namespace QuanLiNhanVien.View.User
{
    /// <summary>
    /// Interaction logic for NavBarUser.xaml
    /// </summary>
    public partial class NavBarUser : Window
    {
        public NavBarUser()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(EmployeeSession.Instance.FullName))
            {
                WelcomeTextBlock.Text = $"{EmployeeSession.Instance.FullName}";
            }
            else
            {
                WelcomeTextBlock.Text = "Welcome, User";
            }
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Profile();
        }

        private void ScheduleWork_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new WorkSchedule();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Notifications();
        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Attendance();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            EmployeeSession.Instance.ClearSession();
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
