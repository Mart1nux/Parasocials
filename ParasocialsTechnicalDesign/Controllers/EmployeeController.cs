using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public EmployeeController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("/createEmployee/{email}")]
        public async Task<IActionResult> CreateEmployee([FromRoute] string email, string password, decimal hourlyPayRate, Guid positionId)
        {
            var position = await dbContext.Positions.FindAsync(positionId);
            if (position != null)
            {
                var employee = new Employee()
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Password = password,
                    HourlyPayRate = hourlyPayRate,
                    Position = position
                };
                await dbContext.Employees.AddAsync(employee);
                await dbContext.SaveChangesAsync();
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/deleteEmployeeById/{employeeId:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeId)
        {
            var employee = await dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/changeEmployee{employeeId:Guid}")]
        public async Task<IActionResult> ChangeEmployee([FromRoute] Guid employeeId, string email, string password, decimal hourlyPayRate, Guid positionId)
        {
            var employee = await dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                var position = await dbContext.Positions.FindAsync(positionId);
                if (position != null)
                {
                    employee.Email = email;
                    employee.Password = password;
                    employee.HourlyPayRate = hourlyPayRate;
                    employee.Position = position;
                    dbContext.SaveChanges();
                    return Ok(employee);
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getEmployeeByEmail/{email}")]
        public async Task<IActionResult> GetEmployeeByEmail([FromRoute] string email)
        {
            var employee = await dbContext.Employees.Where(c => c.Email == email).Include(e => e.Position).FirstOrDefaultAsync();
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getEmployeeById/{employeeId}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid employeeId)
        {
            var employee = await dbContext.Employees.Where(c => c.Id == employeeId).Include(e => e.Position).FirstOrDefaultAsync();
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getEmployees")]
        public async Task<IActionResult> GetEmployeeList()
        {
            return Ok(await dbContext.Employees.Include(e => e.Position).ToListAsync());
        }
    }
}
