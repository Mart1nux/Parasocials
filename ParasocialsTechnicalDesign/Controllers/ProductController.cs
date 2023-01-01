using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public ProductController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("/products")]
        public async Task<IActionResult> GetproductList()
        {
            return Ok(await dbContext.Products.ToListAsync());
        }

        [HttpGet]
        [Route("/product/{productId:guid}")]
        public async Task<IActionResult> Getproduct([FromRoute] Guid productId)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/product")]
        public async Task<IActionResult> Addproduct(string barcode, string name, decimal price)
        {
            var product = new Product()
            {
                ProductId = Guid.NewGuid(),
                Barcode = barcode,
                Name = name,
                Price = price,
                Orders = new List<Order>(),
                Groups = new List<Group>(),
                InventoryId = Guid.NewGuid(),
                Inventory = new Inventory()
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("/ProductUpdateById/{productId:guid}")]
        public async Task<IActionResult> UpdateProductById([FromRoute] Guid productId, string barcode, string name, decimal price)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                product.Barcode = barcode;
                product.Name = name;
                product.Price = price;
                await dbContext.SaveChangesAsync();
                return Ok(product);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/AddProductToGroup/{productId:guid}")]
        public async Task<IActionResult> AddProductToGroup([FromRoute] Guid productId, Guid groupId)
        {
            var product = await dbContext.Products.Include(e => e.Groups).Where(c => c.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                var group = await dbContext.Group.Include(e => e.Products).Where(c => c.GroupId == groupId).FirstOrDefaultAsync();
                if (group != null)
                {
                    group.Products.Add(product);
                    product.Groups.Add(group);
                    await dbContext.SaveChangesAsync();
                    return Ok(product);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/productsDeleteById/{productId:guid}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
        {
            var product = await dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/productsDeleteByName/{name}")]
        public async Task<IActionResult> DeleteProductByName([FromRoute] string name)
        {
            var product = await dbContext.Products.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }


}