using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public TipController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipList()
        {
            return Ok(await dbContext.Tips.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTip([FromRoute] Guid id)
        {
            var tip = await dbContext.Tips.FindAsync(id);
            if (tip != null)
            {
                return Ok(tip);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTip(AddTip addTip)
        {
            var tip = new Tip()
            {
                TipId = Guid.NewGuid(),
                Giver = addTip.Giver,
                Type = addTip.Type,
                GivenDate = DateTime.Now,
                Receiver = addTip.Receiver

            };

            await dbContext.Tips.AddAsync(tip);
            await dbContext.SaveChangesAsync();

            return Ok(tip);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTip([FromRoute] Guid id)
        {
            var tip = await dbContext.Tips.FindAsync(id);
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
