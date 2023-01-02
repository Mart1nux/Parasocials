using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class ShiftController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;

        public ShiftController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

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
                return Ok(_mapper.Map<ShiftDTO>(shift));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/addEmployeeToShift/{shiftId:Guid}")]
        public async Task<IActionResult> AddEmployee([FromRoute] Guid shiftId, Guid employeeId)
        {
            var shift = await dbContext.Shifts.Include(e => e.Employees).Where(c => c.ShiftId == shiftId).FirstOrDefaultAsync();
            if (shift != null)
            {
                var employee = await dbContext.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    shift.Employees.Add(employee);
                    dbContext.SaveChanges();
                    return Ok(_mapper.Map<ShiftDTO>(shift));
                }
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

            return Ok(_mapper.Map<ShiftDTO>(shift));
        }

        [HttpGet]
        [Route("/getShiftById/{shiftId:guid}")]
        public async Task<IActionResult> GetShift([FromRoute] Guid shiftId)
        {
            var shift = await dbContext.Shifts.FindAsync(shiftId);
            if (shift != null)
            {
                return Ok(_mapper.Map<ShiftDTO>(shift));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getShifts")]
        public async Task<IActionResult> GetShiftList()
        {
            return Ok(_mapper.Map<List<ShiftDTO>>(await dbContext.Shifts.ToListAsync()));
        }
    }
}
