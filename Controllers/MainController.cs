using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rencredit_task.Models;

namespace rencredit_task.Controllers
{
    [Route("api/main")]
    public class MainController : Controller
    {
        [HttpGet("offices")]
        [ResponseCache(Duration = 60)]
        public async Task<IEnumerable<Office>> GetOffices()
        {
            using (var db = new ApplicationContext())
            {
                return await db.Offices.ToArrayAsync();
            }
        }

        [HttpGet("timeslots")]
        public async Task<IEnumerable<TimeSlot>> GetTimeSlots(int officeId, DateTime date)
        {
            using (var db = new ApplicationContext())
            {
                var result = await db.TimeSlots.Where(s => s.OfficeId == officeId && s.BeginTime.Date == date.Date).ToListAsync();

                if (result.Count == 0)
                {
                    var office = await db.Offices.Where(o => o.Id == officeId).FirstAsync();
                    var time = date.Date.Add(office.BeginTime);
                    var endTime = date.Date.Add(office.EndTime);

                    while (time < endTime)
                    {
                        var timeSlot = new TimeSlot { BeginTime = time, EndTime = time.AddMinutes(TimeSlot.Interval), Office = office };

                        if (timeSlot.EndTime > endTime)
                            timeSlot.EndTime = endTime;

                        db.TimeSlots.Add(timeSlot);
                        result.Add(timeSlot);
                        time = timeSlot.EndTime;
                    }

                    await db.SaveChangesAsync();
                }

                return result;
            }
        }

        [HttpPut("booking")]
        public async Task<bool> Booking(int timeSlotId)
        {
            using (var db = new ApplicationContext())
            {
                var timeSlot = await db.TimeSlots.Where(s => s.Id == timeSlotId).FirstAsync();
                timeSlot.IsBusy = !timeSlot.IsBusy;
                await db.SaveChangesAsync();
                return timeSlot.IsBusy;
            }
        }
    }
}
