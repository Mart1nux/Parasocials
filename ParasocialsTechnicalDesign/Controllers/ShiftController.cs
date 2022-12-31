using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class ShiftController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public ShiftController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut]
        [Route("/changeShift/{shiftId:Guid}")]
        public async Task<IActionResult> ChangeShift([FromRoute] Guid shiftId, DateTime startTime, DateTime endTime)
        {
            var shift = await dbContext.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                shift.StartTime = startTime;
                shift.EndTime = endTime;
                dbContext.SaveChanges();
                return Ok(shift);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/createShift/{startTime}")]
        public async Task<IActionResult> CreateShift([FromRoute] DateTime startTime, DateTime endTime)
        {
            var shift = new Shift()
            {
                Employee = Guid.NewGuid(),
                ShiftId = Guid.NewGuid(),
                StartTime = startTime,
                EndTime = endTime,
                Employees = new List<Employee>()
            };
            await dbContext.Shifts.AddAsync(shift);
            await dbContext.SaveChangesAsync();

            return Ok(shift);
        }

        [HttpGet]
        [Route("/getShiftById/{shiftId:guid}")]
        public async Task<IActionResult> GetShift([FromRoute] Guid shiftId)
        {
            var shift = await dbContext.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                return Ok(shift);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getShifts")]
        public async Task<IActionResult> GetShiftList()
        {
            return Ok(await dbContext.Shifts.ToListAsync());
        }
    }
}
