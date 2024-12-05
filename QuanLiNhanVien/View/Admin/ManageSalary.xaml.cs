using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Model;
using QuanLiNhanVien.Service;
using QuanLiNhanVien.SessionUser;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;


namespace QuanLiNhanVien.View.Admin
{
    /// <summary>
    /// Interaction logic for ManageSalary.xaml
    /// </summary>
    public partial class ManageSalary : UserControl
    {
        private WorkScheduleService _workScheduleService;
        private EmployeeService _employeeService;
        private SalaryService _salaryService;
        private AttendanceService _attendanceService;
        public ManageSalary()
        {
            InitializeComponent();
            _workScheduleService = new WorkScheduleService();
            _employeeService = new EmployeeService();
            _salaryService = new SalaryService();
            _attendanceService = new AttendanceService();
            LoadEmployeeList();
            LoadSalaryList();
            LoadDataFilter();
        }

        private void LoadSalaryList()
        {
            var listSalary = _salaryService.GetSalaryList();
            SalaryDataGrid.ItemsSource = listSalary;
        }

        private void LoadDataFilter()
        {
            var employeeList = _employeeService.GetEmployees();

            FilterEmployeeComboBox.ItemsSource = employeeList; // Gán trực tiếp danh sách vào ItemsSource
            // Hiển thị thuộc tính FullName của Employee trong ComboBox
            FilterEmployeeComboBox.DisplayMemberPath = "FullName";
            // Nếu muốn giá trị được chọn trả về EmployeeId
            FilterEmployeeComboBox.SelectedValuePath = "Id";

            List<int> months = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            FilterMonthComboBox.ItemsSource = months;

            List<int> years = new List<int>() { 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024 };
            FilterYearComboBox.ItemsSource = years;
        }

        private void LoadEmployeeList()
        {   
            var employeeList = _employeeService.GetEmployees();
            
            EmployeeComboBox.ItemsSource = employeeList; // Gán trực tiếp danh sách vào ItemsSource
            // Hiển thị thuộc tính FullName của Employee trong ComboBox
            EmployeeComboBox.DisplayMemberPath = "FullName";
            // Nếu muốn giá trị được chọn trả về EmployeeId
            EmployeeComboBox.SelectedValuePath = "Id";

            List<int> months = new List<int>() { 1, 2, 3, 4, 5 , 6 ,7, 8, 9, 10, 11, 12};
            MonthComboBox.ItemsSource = months;

            List<int> years = new List<int>() { 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024 };
            YearComboBox.ItemsSource = years;
        }

        private void LoadCalendar(int year, int month, List<Attendance> attendances)
        {
            // Xóa các phần tử cũ trong CalendarGrid nếu có
            CalendarGrid.Children.Clear();

            // Lấy ngày đầu tiên và ngày cuối cùng của tháng
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            BoardWork.Text = "Month " + $"{month}";

            // Duyệt qua các ngày trong tháng
            for (int day = 1; day <= daysInMonth; day++)
            {
                // Tạo TextBlock để hiển thị ngày
                var dayBlock = new TextBlock
                {
                    Text = day.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0),
                    Padding = new Thickness(10),
                    Background = Brushes.LightGray,
                   
                };

                // Kiểm tra xem ngày này có nằm trong danh sách WorkSchedules không
                DateTime currentDay = new DateTime(year, month, day);
                if (attendances.Any(ws => ws.dateAttendance.Date == currentDay))
                {
                    dayBlock.Background = Brushes.LightGreen; // Tô màu ô có công
                }
                // Thêm TextBlock vào CalendarGrid
                CalendarGrid.Children.Add(dayBlock);
            }
        }

        //Tìm bảng chấm công theo id nhân viên
        private void OnFindWorkScheduleClick(object sender, RoutedEventArgs e)
        {
            int employeeId = (int)EmployeeComboBox.SelectedValue;
            int month = (int)MonthComboBox.SelectedItem;
            int year = (int)YearComboBox.SelectedItem;

            // Lấy bảng chấm công
            var attendanceEmployee = _attendanceService.GetAttendanceByMonth(employeeId, year, month);
            int totalShifts = attendanceEmployee.Count;
            int totalSalary = totalShifts * 100000;

            // Hiển thị kết quả
            TotalShiftsText.Text = totalShifts.ToString();
            TotalSalaryText.Text = totalSalary.ToString("N0", new CultureInfo("vi-VN")) + " VNĐ";

            LoadCalendar(year,month, attendanceEmployee);
        }

        //Trả lương cho nhân viên
        private void OnPaySalaryClick(object sender, RoutedEventArgs e)
        {
            int employeeId = (int)EmployeeComboBox.SelectedValue;
            int year = (int)YearComboBox.SelectedValue;
            int month = (int)MonthComboBox.SelectedValue;
            int totalShifts = int.Parse(TotalShiftsText.Text);
            string cleanedText = TotalSalaryText.Text.Replace(".", "").Replace(" VNĐ", "");
            int totalSalary = int.Parse(cleanedText);

            Salary salary = new Salary()
            {
                Month = month,
                Year = year,
                TotalHoursWorked = totalShifts,
                TotalSalary = totalSalary,
                EmployeeId = employeeId,
            };


            _salaryService.AddSalary(salary);

            //Load lại dữ liệu của danh sách lương
            LoadSalaryList();
            // Xóa input sau khi trả lương
            EmployeeComboBox.SelectedIndex = -1;
            MonthComboBox.SelectedIndex = -1;
            YearComboBox.SelectedIndex = -1;
            TotalShiftsText.Text = "";
            TotalSalaryText.Text = "";
        }

        //Lọc dữ liệu 
        private void FilterDataList_Click(object sender, RoutedEventArgs e)
        {
            // Khởi tạo giá trị employeeId, month, year với null (có thể không có giá trị)
            int? employeeId = null;
            int? year = null;
            int? month = null;

            // Nếu người dùng chọn Employee thì lấy giá trị employeeId
            if (FilterEmployeeComboBox.SelectedItem != null)
            {
                employeeId = (int)FilterEmployeeComboBox.SelectedValue;
            }

            // Nếu người dùng chọn Year thì lấy giá trị year
            if (FilterYearComboBox.SelectedItem != null)
            {
                year = (int)FilterYearComboBox.SelectedValue;
            }

            // Nếu người dùng chọn Month thì lấy giá trị month
            if (FilterMonthComboBox.SelectedItem != null)
            {
                month = (int)FilterMonthComboBox.SelectedValue;
            }

            // Lấy dữ liệu đã lọc dựa trên những gì người dùng đã chọn
            SalaryDataGrid.ItemsSource = _salaryService.GetFilterDataSalaryList(employeeId, month, year);

            //Xóa dữ liệu input sau khi lọc xong
            FilterEmployeeComboBox.SelectedIndex = -1;
            FilterMonthComboBox.SelectedIndex = -1;
            FilterYearComboBox.SelectedIndex = -1;
        }

        //Tạo bảng Excel
        private void OnExportToExcelClick(object sender, RoutedEventArgs e)
        {
            // Đảm bảo rằng EPPlus có thể làm việc với file Excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Tạo một Worksheet mới
                var worksheet = package.Workbook.Worksheets.Add("Bảng Chấm Công");

                // Thiết lập tiêu đề bảng
                worksheet.Cells[1, 1].Value = "Ngày";
                worksheet.Cells[1, 3].Value = "Trạng Thái (Làm việc)";

                // Tạo các tiêu đề tô đậm
                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Duyệt qua các ô trong CalendarGrid và xuất dữ liệu
                int rowIndex = 2;
                foreach (var child in CalendarGrid.Children)
                {
                    if (child is TextBlock textBlock)
                    {
                        // Giả sử TextBlock chứa ngày ở định dạng "dd/MM/yyyy" hoặc tương tự
                        if (DateTime.TryParse(textBlock.Text, out DateTime date))
                        {
                            worksheet.Cells[rowIndex, 1].Value = date.Day; // Ngày
                        }
                        else
                        {
                            worksheet.Cells[rowIndex, 1].Value = textBlock.Text; // Ngày nếu không parse được
                        }

                        worksheet.Cells[rowIndex, 3].Value = textBlock.Background == Brushes.LightGreen ? "Làm việc" : "Nghỉ";
                        rowIndex++;
                    }
                }

                // Tự động căn chỉnh độ rộng các cột
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Hiển thị hộp thoại lưu file
                var dialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Lưu bảng chấm công",
                    FileName = "Bảng chấm công tháng " + $"{BoardWork.Text.Substring(6)} - " + $"{EmployeeComboBox.Text}" +".xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    // Lưu file vào đường dẫn được chọn
                    var file = new FileInfo(dialog.FileName);
                    package.SaveAs(file);
                    MessageBox.Show("Xuất dữ liệu thành công!");
                }
            }
        }

        private void SalaryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
