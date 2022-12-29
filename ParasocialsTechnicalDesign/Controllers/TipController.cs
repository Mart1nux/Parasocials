using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class TipController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public TipController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("/getTips")]
        public async Task<IActionResult> GetTipList()
        {
            return Ok(await dbContext.Tips.ToListAsync());
        }

        [HttpGet]
        [Route("/getTipById/{tipId:guid}")]
        public async Task<IActionResult> GetTip([FromRoute] Guid tipId)
        {
            var tip = await dbContext.Tips.FindAsync(tipId);
            if (tip != null)
            {
                return Ok(tip);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/createTip/{giver}")]
        public async Task<IActionResult> AddTip([FromRoute] string giver, TipType tipType, DateTime givenDate, Guid receiver)
        {
            var tip = new Tip()
            {
                TipId = Guid.NewGuid(),
                Giver = giver,
                Type = tipType,
                GivenDate = givenDate,
                Receiver = new Employee()//receiver
            };

            await dbContext.Tips.AddAsync(tip);
            await dbContext.SaveChangesAsync();

            return Ok(tip);
        }

        [HttpDelete]
        [Route("/deleteTipById/{tipId:guid}")]
        public async Task<IActionResult> DeleteTip([FromRoute] Guid tipId)
        {
            var tip = await dbContext.Tips.FindAsync(tipId);
            if (tip != null)
            {
                dbContext.Tips.Remove(tip);
                dbContext.SaveChanges();
                return Ok(tip);
            }
            return NotFound();
        }
    }
}
