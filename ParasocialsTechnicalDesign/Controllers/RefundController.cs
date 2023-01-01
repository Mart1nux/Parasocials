using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class RefundController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;

        public RefundController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

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
            return Ok(_mapper.Map<RefundTicketDTO>(refund));
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
                return Ok(_mapper.Map<RefundTicketDTO>(refund));
            }
            return NotFound();
        }
    }
}