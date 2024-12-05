using Microsoft.EntityFrameworkCore;
using QuanLiNhanVien.Database;
using QuanLiNhanVien.Model;
using QuanLiNhanVien.View.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiNhanVien.Service
{
    class WorkScheduleService
    {
        private readonly AppDbContext _context; 
        private EmployeeService _employeeService;

        public WorkScheduleService()
        {
            _context = new AppDbContext();
            _employeeService = new EmployeeService();
        }

        // Phương thức thêm lịch làm việc cho nhân viên
        public void AddWorkSchedule(int employeeId, DateTime startDate, string shift)
        {
            int totalDays = 180;  // Khoảng 6 tháng

            // Lặp lại từ ngày bắt đầu và tạo lịch trong 6 tháng
            for (int i = 0; i < totalDays; i++)
            {
                DateTime currentDate = startDate.AddDays(i);

                // Nếu là Chủ nhật thì bỏ qua
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    // Kiểm tra xem lịch đã tồn tại chưa
                    var existingSchedule = _context.WorkSchedules
                        .FirstOrDefault(ws => ws.EmployeeId == employeeId && ws.WorkDate.Date == currentDate.Date);

                    if (existingSchedule == null)
                    {
                        // Nếu chưa tồn tại, thêm lịch mới
                        var workSchedule = new Model.WorkSchedule
                        {
                            EmployeeId = employeeId,
                            WorkDate = currentDate.Date,
                            Shift = shift
                        };

                        _context.WorkSchedules.Add(workSchedule);
                    }
                    else
                    {
                        // Nếu đã tồn tại, có thể cập nhật lịch làm
                        existingSchedule.Shift = shift;
                        _context.WorkSchedules.Update(existingSchedule);
                    }
                }
            }

            // Sau khi hoàn tất, lưu tất cả thay đổi vào database
            _context.SaveChanges();
        }

        // Lấy lịch làm của tất cả nhân viên trong một tuần
        public List<Model.WorkSchedule> GetWorkSchedulesAllEmployeesByWeek(DateTime startDate)
        {
            DateTime endDate = startDate.AddDays(6); // Một tuần có 7 ngày

            // Lấy tất cả lịch làm trong khoảng thời gian từ startDate đến endDate
            return _context.WorkSchedules
                .Where(ws => ws.WorkDate >= startDate && ws.WorkDate <= endDate)
                .Include(ws => ws.Employee)
                .ToList();
        }

        // Lấy lịch làm của một nhân viên dựa theo ID
        public List<Model.WorkSchedule> GetWorkScheduleByEmployeeId(int employeeId, DateTime startDate)
        {
            Employee employeeFound = _employeeService.GetEmployee(employeeId);
            DateTime endDate = startDate.AddDays(6);
            return _context.WorkSchedules
                .Where(ws => ws.Employee == employeeFound &&  ws.WorkDate >= startDate && ws.WorkDate <= endDate)
                .Include(ws => ws.Employee)
                .ToList();
        }

        //Lấy lịch làm việc của một nhân viên dựa theo tháng 
        public List<Model.WorkSchedule> GetWorkSchedulesByMonth(int employeeId, int year, int month)
        {
            return _context.WorkSchedules
                .Where(ws => ws.EmployeeId == employeeId
                          && ws.WorkDate.Year == year
                          && ws.WorkDate.Month == month)
                .ToList();
        }

        // Xóa lịch làm của một nhân viên
        public void DeleteWorkSchedule(int employeeId, DateTime date, string shift)
        {
            Employee employeeFound = _employeeService.GetEmployee(employeeId);
            var workSchedule = _context.WorkSchedules
                .FirstOrDefault(ws => ws.Employee == employeeFound && ws.WorkDate  == date && ws.Shift == shift);

            if (workSchedule != null)
            {
                _context.WorkSchedules.Remove(workSchedule);
                _context.SaveChanges();
            }
        }

        //Tự động thêm lịch làm việc
        public void AutoAssignShifts(int employeeId)
        {
            var shifts = new[] { "Morning", "Afternoon", "Evening" };
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(6);
            int totalDays = (endDate - startDate).Days;
            var random = new Random();

            for (int i = 0; i <= totalDays; i++)
            {
                var currentDate = startDate.AddDays(i);

                // Chọn ngẫu nhiên ca ban đầu
                int shiftIndex = random.Next(shifts.Length);

                // Thử lần lượt các ca nếu ca đầu tiên đã đủ 2 người
                for (int attempt = 0; attempt < shifts.Length; attempt++)
                {
                    var shift = shifts[(shiftIndex + attempt) % shifts.Length]; // Thử ca tiếp theo

                    // Kiểm tra số người trong ca này
                    var shiftCount = _context.WorkSchedules
                        .Count(ws => ws.WorkDate == currentDate.Date && ws.Shift == shift);

                    if (shiftCount < 2) // Nếu ca chưa đủ 2 người
                    {
                        _context.WorkSchedules.Add(new Model.WorkSchedule
                        {
                            EmployeeId = employeeId,
                            WorkDate = currentDate.Date,
                            Shift = shift,
                        });
                        break; // Thêm xong thì thoát vòng lặp
                    }
                }
            }

            _context.SaveChanges();
        }

        public Model.WorkSchedule FindByDate(DateTime date, int employeeId)
        {
            var existingSchedule = _context.WorkSchedules
                .FirstOrDefault(ws => ws.EmployeeId == employeeId
                           && EF.Functions.DateDiffDay(ws.WorkDate, date) == 0);
            if (existingSchedule != null)
            {
                return existingSchedule;
            }
            return null;
        }


        public DateTime StartOfWeek(DateTime startDate, DayOfWeek startDayOfWeek)
        {
            // Tính số ngày cần lùi về để đến được ngày "startDayOfWeek" (thứ Hai trong trường hợp này)
            int diff = (7 + (startDate.DayOfWeek - startDayOfWeek)) % 7;
            // Trừ đi số ngày để trả về ngày thứ Hai của tuần chứa startDate
            return startDate.AddDays(-1 * diff).Date;
        }
    }
}
