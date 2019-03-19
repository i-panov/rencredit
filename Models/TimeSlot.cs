using System;

namespace rencredit_task.Models
{
    public class TimeSlot
    {
        public const int Interval = 30;
        public int Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBusy { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}