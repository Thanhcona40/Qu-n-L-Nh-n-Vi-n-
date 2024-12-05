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

namespace QuanLiNhanVien.View.Admin
{
    /// <summary>
    /// Interaction logic for ManageWorkSchedule.xaml
    /// </summary>
    public partial class ManageWorkSchedule : UserControl
    {
        private WorkScheduleService _workScheduleService;
        private EmployeeService _employeeService;
        DateTime startDate = new DateTime(2024, 9, 2);
        public ManageWorkSchedule()
        {
            InitializeComponent();
            _workScheduleService = new WorkScheduleService();
            _employeeService = new EmployeeService();
            LoadWorkSchedule(startDate);
        }

        //Tạo lịch làm việc mới
        private void AddShift_Click(object sender, RoutedEventArgs e)
        {
            int employeeId;
            if (!int.TryParse(EmployeeIdTextBox.Text, out employeeId))
            {
                MessageBox.Show("Invalid Employee ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedShift = (ShiftComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var selectedDate = DatePicker.SelectedDate;

            if (selectedShift != null && selectedDate != null)
            {
              _workScheduleService.AddWorkSchedule(employeeId, selectedDate.Value, selectedShift); 

                // Load lại lịch làm
                LoadWorkSchedule(selectedDate.Value);
            }
            else
            {
                MessageBox.Show("Please select all inputs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadWorkSchedule(DateTime date)
        {
            var startWeek = _workScheduleService.StartOfWeek(date, DayOfWeek.Monday);

            // Cập nhật ngày tháng trong tuần
            for (int i = 0; i < 7; i++)
            {
                var currentDate = startWeek.AddDays(i).ToString("dd/MM");
                switch (i)
                {
                    case 0: Day1.Text = currentDate; break;
                    case 1: Day2.Text = currentDate; break;
                    case 2: Day3.Text = currentDate; break;
                    case 3: Day4.Text = currentDate; break;
                    case 4: Day5.Text = currentDate; break;
                    case 5: Day6.Text = currentDate; break;
                    case 6: Day7.Text = currentDate; break;
                }
            }

            // Xóa tất cả StackPanel cũ trước khi load dữ liệu mới
            var panelsToRemove = ScheduleGrid.Children.OfType<StackPanel>().ToList();
            foreach (var panel in panelsToRemove)
            {
                ScheduleGrid.Children.Remove(panel);
            }

            // Lấy lịch làm việc theo tuần
            var workSchedules = _workScheduleService.GetWorkSchedulesAllEmployeesByWeek(startWeek);

            // Hiển thị lịch làm việc của nhân viên
            foreach (var schedule in workSchedules)
            {
                if (schedule != null)
                {
                    int columnIndex = (schedule.WorkDate - date).Days + (int)date.DayOfWeek + 1; // Tính cột theo ngày
                    int rowIndex = 0;
                    switch (schedule.Shift)
                    {
                        case "Morning": rowIndex = 1; break;
                        case "Afternoon": rowIndex = 2; break;
                        case "Evening": rowIndex = 3; break;
                    }

                    // Kiểm tra xem StackPanel đã tồn tại chưa
                    var existingStackPanel = ScheduleGrid.Children
                        .OfType<StackPanel>()
                        .FirstOrDefault(sp => Grid.GetRow(sp) == rowIndex && Grid.GetColumn(sp) == columnIndex);

                    // Tạo TextBlock hiển thị tên nhân viên
                    TextBlock employeeTextBlock = new TextBlock
                    {
                        Text = schedule.Employee.FullName,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontSize = 10,
                        Margin = new Thickness(0, 2, 0, 2),
                        Tag = "EmployeeName"
                    };

                    if (existingStackPanel == null)
                    {
                        // Nếu chưa có StackPanel, tạo mới và thêm vào ScheduleGrid
                        existingStackPanel = new StackPanel
                        {
                            Orientation = Orientation.Vertical,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top,
                            Width = 85,
                            Height = 65,
                            Margin = new Thickness(10)
                        };

                        Grid.SetRow(existingStackPanel, rowIndex);
                        Grid.SetColumn(existingStackPanel, columnIndex);
                        ScheduleGrid.Children.Add(existingStackPanel);
                    }

                    // Thêm TextBlock vào StackPanel (dù có mới hay đã tồn tại)
                    existingStackPanel.Children.Add(employeeTextBlock);
                }
                else
                {
                    MessageBox.Show("Not found data");
                }
            }

        }


        //Sang tuần tiếp theo
        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            startDate = startDate.AddDays(7);
            LoadWorkSchedule(startDate);
        }

        //Lùi về tuần trước
        private void PreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            startDate = startDate.AddDays(-7);
            LoadWorkSchedule(startDate);

        }
    
    }
}
