using QuanLiNhanVien.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FontAwesome.WPF;

namespace QuanLiNhanVien.View.Notification
{
    /// <summary>
    /// Interaction logic for Nofications.xaml
    /// </summary>
    public partial class Notifications : UserControl
    {
        private NotificationService _notificationService;

        public Notifications()
        {
            InitializeComponent();
            _notificationService = new NotificationService();
            LoadNotifications();
        }

        // Hàm tải thông báo từ NotificationService và hiển thị lên giao diện
        private void LoadNotifications()
        {
            var notifications = _notificationService.GetAllNotifications();

            foreach (var notification in notifications)
            {
                // Tạo khối thông báo
                var notificationBlock = new Border
                {
                    Background = Brushes.LightGray,
                    BorderBrush = Brushes.DarkGray,
                    BorderThickness = new Thickness(1),
                    Padding = new Thickness(10),
                    Margin = new Thickness(5),
                    CornerRadius = new CornerRadius(5),
                    Width = 600,
                    Height = 100,
                    Opacity = 0.9,
                };

                Grid notificationGrid = new Grid();
                notificationGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) }); // Icon chiếm 2 phần
                notificationGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) }); // Text chiếm 8 phần

                Grid RowGrid = new Grid();
                RowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Hàng 1
                RowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Hàng 2
                RowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Hàng 3
                RowGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Hàng 4
                //Hiển thị icon Chuông
                var bellIcon = new ImageAwesome
                {
                    Icon = FontAwesomeIcon.Bell,
                    Width = 24,
                    Height = 24,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 0, 10, 0), // Khoảng cách giữa icon và text
                };
                Grid.SetColumn(bellIcon, 0);
                notificationGrid.Children.Add(bellIcon);
                // Hiển thị tên người tạo
                var createdByText = new TextBlock
                {
                    Text = $"Created by: {notification.Employee.FullName}",
                    FontWeight = FontWeights.Bold
                };
                Grid.SetRow(createdByText, 0);
                RowGrid.Children.Add(createdByText);
                // Hiển thị tiêu đề thông báo
                var titleText = new TextBlock
                {
                    Text = notification.Title,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Black),
                };
                Grid.SetRow(titleText, 1);
                RowGrid.Children.Add(titleText);
                var contentText = new TextBlock
                {
                    Text = notification.Content,
                    FontSize = 15,
                    Foreground = new SolidColorBrush(Colors.White),
                };
                Grid.SetRow(contentText, 2);
                RowGrid.Children.Add(contentText);

                var createdAtText = new TextBlock
                {
                    Text = notification.CreatedAt.ToString("dd/MM/yyyy"),
                    FontSize = 15,
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Right,
                };
                Grid.SetRow(createdAtText, 4);
                RowGrid.Children.Add(createdAtText);

                Grid.SetColumn(RowGrid, 1);
                notificationGrid.Children.Add(RowGrid);

                notificationBlock.Child = notificationGrid;

                // Thêm block vào danh sách thông báo
                NotificationPanel.Children.Add(notificationBlock);
            }
        }

        // Hàm mở cửa sổ tạo thông báo mới
        private void AddNofication_Click(object sender, RoutedEventArgs e)
        {
            CreateNotification dialog = new CreateNotification();

            if (dialog.ShowDialog() == true) // Chỉ thực hiện khi người dùng tạo thông báo thành công
            {
                // Refresh lại giao diện thông báo nếu cần
                LoadNotifications();
            }
        }
    }
}
