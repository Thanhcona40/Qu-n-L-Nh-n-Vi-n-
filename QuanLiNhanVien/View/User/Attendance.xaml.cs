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
    /// Interaction logic for Attendance.xaml
    /// </summary>
    public partial class Attendance : UserControl
    {
        private WorkScheduleService workScheduleService;
        private AttendanceService attendanceService;
        public string CurrentDate { get; set; }
        public string CurrentShift { get; set; }
        public string EmployeeName { get; set; }
        public Attendance()
        {
            InitializeComponent();
            workScheduleService = new WorkScheduleService();
            attendanceService = new AttendanceService();
            LoadData();
        }

        private void LoadData()
        {
            DateNow.Text = DateTime.Now.ToString("dd/MM/yyyy");
            int Id = EmployeeSession.Instance.Id;
            DateTime dateTime = DateTime.Now;
            var workSchedule = workScheduleService.FindByDate(dateTime, Id);
            Shift.Text = workSchedule.Shift; // Thay bằng giá trị lấy từ WorkSchedule
            Employee.Text = EmployeeSession.Instance.FullName; // Thay bằng tên người đăng nhập
        }

        private void AttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = EmployeeSession.Instance.Id;
            DateTime dateTime = DateTime.Now;

            var workSchedule = workScheduleService.FindByDate(dateTime, Id);
            if (workSchedule != null)
            {
                var attendance = new Model.Attendance
                {
                    dateAttendance = dateTime,
                    EmployeeId = Id,
                };
                attendanceService.AddAttendance(attendance);
                MessageBox.Show("Attendance Successful!");
            }
        }
    }
}
