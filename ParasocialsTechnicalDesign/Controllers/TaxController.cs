using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using System.Text.RegularExpressions;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class TaxController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;
        public TaxController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/tax")]
        public async Task<IActionResult> AddTax( decimal amount, TaxType type, TaxReason reason, Guid groupId)
        {
            var group = await dbContext.Group.FindAsync(groupId);
            if(group != null)
            {
                if (group.Tax == null)
                {
                    var tax = new Tax()
                    {
                        TaxId = new Guid(),
                        Amount = amount,
                        Reason = reason,
                        Type = type,
                        GroupId = groupId,
                        Group = group
                    };

                    await dbContext.Taxes.AddAsync(tax);
                    await dbContext.SaveChangesAsync();

                    return Ok(_mapper.Map<Position>(tax));
                }
                return Unauthorized();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/taxes")]
        public async Task<IActionResult> GetTaxList()
        {
            return Ok(_mapper.Map<List<PositionDTO>>(await dbContext.Taxes.Include(e => e.Group).ToListAsync()));
        }

        [HttpGet]
        [Route("/tax/{orderId:Guid}")]
        public async Task<IActionResult> GetTaxById([FromRoute] Guid orderId)
        {
            var order = await dbContext.Orders.Include(e => e.Products).Where(c => c.OrderId == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                var products = new List<Product>();
                foreach (var product in order.Products)
                {
                    products.AddRange(await dbContext.Products.Include(e => e.Groups).Where(c => c.ProductId == product.ProductId).ToListAsync());
                }
                if(products.Count > 0)
                {
                    var groups = new List<Models.Group>();
                    foreach (var product in products)
                    {
                        foreach (var group in product.Groups)
                        {
                            groups.AddRange(await dbContext.Group.Include(e => e.Tax).Where(c => c.GroupId == group.GroupId).ToListAsync());
                        }
                    }
                    if(groups.Count > 0)
                    {
                        var taxes = new List<Tax>();
                        foreach (var group in groups)
                        {
                            taxes.Add(await dbContext.Taxes.Where(c => c.TaxId == group.Tax.TaxId).FirstOrDefaultAsync());
                        }
                        if(taxes.Count > 0)
                        {
                            return Ok(_mapper.Map<List<PositionDTO>>(taxes));
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/TaxDeleteByOrder/{orderId:Guid}")]
        public async Task<IActionResult> DeleteATaxByOrderId([FromRoute] Guid orderId)
        {
            var order = await dbContext.Orders.Include(e => e.Products).Where(c => c.OrderId == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                var products = new List<Product>();
                foreach (var product in order.Products)
                {
                    products.AddRange(await dbContext.Products.Include(e => e.Groups).Where(c => c.ProductId == product.ProductId).ToListAsync());
                }
                if (products.Count > 0)
                {
                    var groups = new List<Models.Group>();
                    foreach (var product in products)
                    {
                        foreach (var group in product.Groups)
                        {
                            groups.AddRange(await dbContext.Group.Include(e => e.Tax).Where(c => c.GroupId == group.GroupId).ToListAsync());
                        }
                    }
                    if (groups.Count > 0)
                    {
                        var taxes = new List<Tax>();
                        foreach (var group in groups)
                        {
                            taxes.Add(await dbContext.Taxes.Where(c => c.TaxId == group.Tax.TaxId).FirstOrDefaultAsync());
                        }
                        if (taxes.Count > 0)
                        {
                            dbContext.Taxes.RemoveRange(taxes);
                            dbContext.SaveChanges();
                            return Ok(_mapper.Map<List<PositionDTO>>(taxes));
                        }
                    }
                }
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/TaxUpdateByOrder/{orderId:Guid}")]
        public async Task<IActionResult> UpdateATaxByOrderId([FromRoute] Guid orderId, decimal amount, TaxType type, TaxReason reason)
        {
            var order = await dbContext.Orders.Include(e => e.Products).Where(c => c.OrderId == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                var products = new List<Product>();
                foreach (var product in order.Products)
                {
                    products.AddRange(await dbContext.Products.Include(e => e.Groups).Where(c => c.ProductId == product.ProductId).ToListAsync());
                }
                if (products.Count > 0)
                {
                    var groups = new List<Models.Group>();
                    foreach (var product in products)
                    {
                        foreach (var group in product.Groups)
                        {
                            groups.AddRange(await dbContext.Group.Include(e => e.Tax).Where(c => c.GroupId == group.GroupId).ToListAsync());
                        }
                    }
                    if (groups.Count > 0)
                    {
                        var taxes = new List<Tax>();
                        foreach (var group in groups)
                        {
                            taxes.Add(await dbContext.Taxes.Where(c => c.TaxId == group.Tax.TaxId).FirstOrDefaultAsync());
                        }
                        if (taxes.Count > 0)
                        {
                            foreach(var tax in taxes)
                            {
                                tax.Amount = amount;
                                tax.Type = type;
                                tax.Reason = reason;
                            }
                            dbContext.SaveChanges();
                            return Ok(_mapper.Map<List<PositionDTO>>(taxes));
                        }
                    }
                }
            }
            return NotFound();
        }
    }
}
