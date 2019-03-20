using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace rencredit_task.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [JsonIgnore]
        public List<TimeSlot> TimeSlots { get; set; }
    }
}
