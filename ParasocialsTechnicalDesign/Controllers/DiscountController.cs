using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;

        public DiscountController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/discount")]
        public async Task<IActionResult> AddDiscount(DiscountType type, decimal ammount, DateTime discountStart, DateTime discountEnd, Guid groupId)
        {
            var group = await dbContext.Group.FindAsync(groupId);
            var discount = new Discount
            {
                DiscountId = Guid.NewGuid(),
                Group = group,
                Type = type,
                Ammount = ammount,
                DiscountStart = discountStart,
                DiscountEnd = discountEnd
            };
            await dbContext.Discounts.AddAsync(discount);
            await dbContext.SaveChangesAsync();
            return Ok(_mapper.Map<DiscountDTO>(discount));
        }

        [HttpGet]
        [Route("/discounts")]
        public async Task<IActionResult> GetDiscounts()
        {
            return Ok(_mapper.Map<List<DiscountDTO>>(await dbContext.Discounts.Include(e => e.Group).ToListAsync()));
        }

        [HttpGet]
        [Route("/discount/{productGroupName}")]
        public async Task<IActionResult> GetDiscountByGroupName([FromRoute] string productGroupName)
        {
            var discount = await dbContext.Discounts.Include(e => e.Group).Where(c => c.Group.GroupName == productGroupName).FirstOrDefaultAsync();
            if (discount != null)
            {
                return Ok(_mapper.Map<DiscountDTO>(discount));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/discountUpdateByProductGroup/{productGroupName}")]
        public async Task<IActionResult> UpdateDiscountByGroupName([FromRoute] string productGroupName, DiscountType type, decimal ammount, DateTime discountStart, DateTime discountEnd)
        {
            var discount = await dbContext.Discounts.Include(e => e.Group).Where(c => c.Group.GroupName == productGroupName).FirstOrDefaultAsync();
            if (discount != null)
            {
                discount.Type = type;
                discount.Ammount = ammount;
                discount.DiscountStart = discountStart;
                discount.DiscountEnd = discountEnd;
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<DiscountDTO>(discount));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/discountDeleteByProductGroup/{productGroupName}")]
        public async Task<IActionResult> DeleteDiscountByGroupName([FromRoute] string productGroupName)
        {
            var discount = await dbContext.Discounts.Where(c => c.Group.GroupName == productGroupName).FirstOrDefaultAsync();
            var group = await dbContext.Group.Where(c => c.GroupName == productGroupName).FirstOrDefaultAsync();
            if (discount != null)
            {
                group.Discount = new Discount()
                {
                    DiscountId = new Guid()
                };
                dbContext.Discounts.Remove(discount);
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<DiscountDTO>(discount));
            }
            return NotFound();
        }

    }


}