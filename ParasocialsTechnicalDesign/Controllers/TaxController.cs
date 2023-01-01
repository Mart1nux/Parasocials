using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class TaxController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public TaxController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("/tax")]
        public async Task<IActionResult> AddTax([FromRoute] decimal amount, TaxType type, TaxReason reason)
        {
            var tax = new Tax()
            {
                TaxId = new Guid(),
                GroupId = groupId,
                Amount = amount,
                Reason = reason,
                Type = type
            };

            var group = new Group()
            {
                GroupId = groupId,
                GroupName = "Group",
                DiscountId = new Guid(),
                
            };

            tax.GroupId = group.GroupId;
            tax.Group = group;

            await dbContext.Taxes.AddAsync(tax);
            await dbContext.SaveChangesAsync();

            return Ok(tax);
        }

        [HttpPost]
        [Route("/taxes")]
        public async Task<IActionResult> GetTaxList()
        {
            return Ok(await dbContext.Taxes.Include(e => e.Group).ToListAsync());
        }
    }
}
