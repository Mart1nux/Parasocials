using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class TipController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;


        public TipController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("/getTips")]
        public async Task<IActionResult> GetTipList()
        {
            return Ok(_mapper.Map<List<TipDTO>>(await dbContext.Tips.Include(e => e.Receiver).ToListAsync()));
        }

        [HttpGet]
        [Route("/getTipById/{tipId:guid}")]
        public async Task<IActionResult> GetTip([FromRoute] Guid tipId)
        {
            var tip = await dbContext.Tips.Include(e => e.Receiver).Where(c => c.TipId == tipId).FirstOrDefaultAsync();
            if (tip != null)
            {
                return Ok(_mapper.Map<TipDTO>(tip));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/createTip/{giver}")]
        public async Task<IActionResult> AddTip([FromRoute] string giver, TipType tipType, DateTime givenDate, Guid receiver)
        {
            var employee = await dbContext.Employees.FindAsync(receiver);
            if(employee != null)
            {
                var tip = new Tip()
                {
                    TipId = Guid.NewGuid(),
                    Giver = giver,
                    Type = tipType,
                    GivenDate = givenDate,
                    Receiver = employee
                };

                await dbContext.Tips.AddAsync(tip);
                await dbContext.SaveChangesAsync();

                return Ok(_mapper.Map<TipDTO>(tip));
            }
            return NotFound();
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
                return Ok(_mapper.Map<TipDTO>(tip));
            }
            return NotFound();
        }
    }
}
