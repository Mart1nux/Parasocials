using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using System;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class PositionController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public PositionController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("/getPositions")]
        public async Task<IActionResult> GetPositionList()
        {
            return Ok(await dbContext.Positions.ToListAsync());
        }

        [HttpGet]
        [Route("/getPositionsById/{positionId:guid}")]
        public async Task<IActionResult> GetPositionById([FromRoute] Guid positionId)
        {
            var position = await dbContext.Positions.FindAsync(positionId);
            if (position != null)
            {
                return Ok(position);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getPositionsByTitle/{title}")]
        public async Task<IActionResult> GetPositionByTitle([FromRoute] string title)
        {
            var position = await dbContext.Positions.Where(c => c.Title == title).FirstOrDefaultAsync();
            if (position != null)
            {
                return Ok(position);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/createPosition/{title}")]
        public async Task<IActionResult> AddPosition([FromRoute] string title, string descritpion, int permissions, int accesToObjects)
        {
            if (Enum.IsDefined(typeof(PositionPermisions), permissions) && Enum.IsDefined(typeof(PositionAccessToObjects), accesToObjects))
            {
                var position = new Position()
                {
                    PositionId = Guid.NewGuid(),
                    Title = title,
                    Description = descritpion,
                    Permisions = (PositionPermisions)permissions,
                    AccessToObjects = (PositionAccessToObjects)accesToObjects

                };

                await dbContext.Positions.AddAsync(position);
                await dbContext.SaveChangesAsync();

                return Ok(position);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/changePosition/{positionId:Guid}")]
        public async Task<IActionResult> ChangePosition([FromRoute] Guid positionId, string title, string descritpion, int permissions, int accesToObjects)
        {
            var position = await dbContext.Positions.FindAsync(positionId);
            if (position != null)
            {
                if (Enum.IsDefined(typeof(PositionPermisions), permissions) && Enum.IsDefined(typeof(PositionAccessToObjects), accesToObjects))
                {
                    position.PositionId = positionId;
                    position.Title = title;
                    position.Description = descritpion;
                    position.Permisions = (PositionPermisions)permissions;
                    position.AccessToObjects = (PositionAccessToObjects)accesToObjects;
                    dbContext.SaveChanges();
                    return Ok(position);
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/deletePositionById/{positionId:guid}")]
        public async Task<IActionResult> DeletePosition([FromRoute] Guid positionId)
        {
            var position = await dbContext.Positions.FindAsync(positionId);
            if (position != null)
            {
                dbContext.Positions.Remove(position);
                dbContext.SaveChanges();
                return Ok(position);
            }
            return NotFound();
        }
    }
}
