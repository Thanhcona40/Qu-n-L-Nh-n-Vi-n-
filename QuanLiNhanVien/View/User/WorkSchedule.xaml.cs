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
    /// Interaction logic for WorkSchedule.xaml
    /// </summary>
    public partial class WorkSchedule : UserControl
    {
        private WorkScheduleService _workScheduleService;
        private EmployeeService _employeeService;
        DateTime startDate = new DateTime(2024, 6, 15);
        public WorkSchedule()
        {
            InitializeComponent();
            _workScheduleService = new WorkScheduleService();
            _employeeService = new EmployeeService();
            LoadWorkSchedule(startDate);
        }

        private void EmployeeIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void LoadWorkSchedule(DateTime date)
        {
            var startWeek = _workScheduleService.StartOfWeek(date, DayOfWeek.Monday);
            for (int i = 0; i < 7; i++)
            {
                // Tính toán ngày hiện tại của tuần (cộng thêm số ngày tương ứng)
                var currentDate = startWeek.AddDays(i).ToString("dd/MM");

                // Gán giá trị vào các TextBlock tương ứng (đã đặt tên trong XAML)
                switch (i)
                {
                    case 0:
                        Day1.Text = currentDate;
                        break;
                    case 1:
                        Day2.Text = currentDate;
                        break;
                    case 2:
                        Day3.Text = currentDate;
                        break;
                    case 3:
                        Day4.Text = currentDate;
                        break;
                    case 4:
                        Day5.Text = currentDate;
                        break;
                    case 5:
                        Day6.Text = currentDate;
                        break;
                    case 6:
                        Day7.Text = currentDate;
                        break;
                }
            }

            foreach (var child in ScheduleGrid.Children.OfType<FrameworkElement>().ToList())
            {
                if (child.Tag != null && child.Tag.ToString() == "EmployeeName")
                {
                    ScheduleGrid.Children.Remove(child);
                }
            }
            int employeeId = EmployeeSession.Instance.Id;

            // Lấy lịch làm việc theo tuần từ WorkScheduleService
            var workSchedules = _workScheduleService.GetWorkScheduleByEmployeeId(employeeId, startWeek);

            // Hiển thị lịch làm việc của nhân viên trong GridView
            foreach (var schedule in workSchedules)
            {
                if (schedule != null)
                {
                    // Tìm cột (ngày) trong tuần dựa vào schedule.Date
                    int columnIndex = (schedule.WorkDate - date).Days + (int)date.DayOfWeek + 1;  // Cột 2 là ngày đầu tiên (thứ hai)

                    // Xác định dòng (ca làm) dựa vào shift (ví dụ: 0 = sáng, 1 = chiều, 2 = tối)
                    int rowIndex = 0;
                    switch (schedule.Shift)
                    {
                        case "Morning":
                            rowIndex = 1;  // Ca sáng
                            break;
                        case "Afternoon":
                            rowIndex = 2;  // Ca chiều
                            break;
                        case "Evening":
                            rowIndex = 3;  // Ca tối
                            break;
                    }

                    // Tạo TextBlock mới để hiển thị tên nhân viên
                    TextBlock employeeTextBlock = new TextBlock
                    {
                        Text = schedule.Employee.FullName,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 85,
                        Height = 65,
                        FontSize = 10,
                        Margin = new Thickness(10, 10, 0, 0),
                        Tag = "EmployeeName",
                    };

                    Grid.SetRow(employeeTextBlock, rowIndex);
                    Grid.SetColumn(employeeTextBlock, columnIndex);
                    ScheduleGrid.Children.Add(employeeTextBlock);
                }
                else
                {
                    MessageBox.Show("Not found data");
                }
            }
        }



        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            startDate = startDate.AddDays(7);
            LoadWorkSchedule(startDate);
        }

        private void PreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            startDate = startDate.AddDays(-7);
            LoadWorkSchedule(startDate);

        }

    }
}
