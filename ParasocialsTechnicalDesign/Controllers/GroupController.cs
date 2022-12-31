using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class GroupController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public GroupController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("/createGroup/{groupName}")]
        public async Task<IActionResult> AddGroup([FromRoute] string groupName)
        {

            var group = new Group()
            {
                GroupId = new Guid(),
                GroupName = groupName,
                Products = new List<Product>()
            };

            await dbContext.Group.AddAsync(group);
            await dbContext.SaveChangesAsync();

            return Ok(group);
        }

        [HttpDelete]
        [Route("/deleteGroupById/{groupId:guid}")]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid groupId)
        {
            var group = await dbContext.Group.FindAsync(groupId);
            if (group != null)
            {
                dbContext.Group.Remove(group);
                dbContext.SaveChanges();
                return Ok(group);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getGroups")]
        public async Task<IActionResult> GetGroupList()
        {
            return Ok(await dbContext.Group.ToListAsync());
        }
    }
}
