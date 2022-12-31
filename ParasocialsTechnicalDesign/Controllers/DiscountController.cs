using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class DiscountController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public DiscountController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("/discount")]
        public async Task<IActionResult> AddDiscount(DiscountType type, decimal ammount, DateTime discountStart, DateTime discountEnd, string groupName)
        {
            var discount = new Discount
            {
                DiscountId = Guid.NewGuid(),
                Group = new Models.Group()
                {    
                    GroupId = new Guid(),
                    GroupName = groupName,
                    Products = new List<Product>()
                },  
                Type = type,
                Ammount = ammount,
                DiscountStart = discountStart,
                DiscountEnd = discountEnd
            };
            await dbContext.Discounts.AddAsync(discount);
            await dbContext.SaveChangesAsync();
            return Ok(discount);
        }

        [HttpGet]
        [Route("/discounts")]
        public async Task<IActionResult> GetDiscounts()
        {
            return Ok(await dbContext.Discounts.Include(e => e.Group).ToListAsync());
        }

        [HttpGet]
        [Route("/discount/{productGroupName}")]
        public async Task<IActionResult> GetDiscountByGroupName([FromRoute] string productGroupName)
        {
            var discount = await dbContext.Discounts.Include(e => e.Group).Where(c => c.Group.GroupName == productGroupName).FirstOrDefaultAsync();
            if (discount != null)
            {
                return Ok(discount);
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
                return Ok(discount);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/discountDeleteByProductGroup/{productGroupName}")]
        public async Task<IActionResult> DeleteDiscountByGroupName([FromRoute] string productGroupName)
        {
            var discount = await dbContext.Discounts.Include(e => e.Group).Where(c => c.Group.GroupName == productGroupName).FirstOrDefaultAsync();
            if (discount != null)
            {
                dbContext.Discounts.Remove(discount);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

    }


}