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
using System.Windows.Shapes;

namespace QuanLiNhanVien.View.Notification
{
    /// <summary>
    /// Interaction logic for CreateNotification.xaml
    /// </summary>
    public partial class CreateNotification : Window
    {
        private NotificationService _notificationService;
        public CreateNotification()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string title = TitleTextBox.Text;
            string content = ContentTextBox.Text;

            // Kiểm tra nếu người dùng nhập đủ thông tin
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Please fill in both Title and Content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Thêm vào danh sách thông báo
                _notificationService.AddNotification(title,content,EmployeeSession.Instance.Id);
                MessageBox.Show("Notification created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
