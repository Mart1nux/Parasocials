using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class RefundController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        public RefundController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("/refund")]
        public async Task<IActionResult> CreateRefund(Guid order, string reason, RefundTicketRefundType refundType)
        {
            var refund = new RefundTicket
            {
                RefundTicketId = Guid.NewGuid(),
                Order = order,
                RequestDate = DateTime.Now,
                Granted = false,
                Reason = reason,
                RefundType = refundType
            };
            await dbContext.RefundTickets.AddAsync(refund);
            await dbContext.SaveChangesAsync();
            return Ok(refund);
        }

        [HttpPost]
        [Route("/refund/{refundTicketId:guid}/{refundType}")]
        public async Task<IActionResult> ApproveRefund([FromRoute] Guid refundTicketId, [FromRoute] RefundTicketRefundType refundType)
        {
            var refund = await dbContext.RefundTickets.FindAsync(refundTicketId);
            if (refund != null)
            {
                refund.Granted = true;
                refund.RefundType = refundType;
                await dbContext.SaveChangesAsync();
                return Ok(refund);
            }
            return NotFound();
        }
    }
}