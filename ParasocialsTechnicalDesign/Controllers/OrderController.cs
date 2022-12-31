using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;


namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;

        public OrderController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("/orders")]
        public async Task<IActionResult> GetOrderList()
        {
            return Ok(_mapper.Map<List<OrderDTO>>(await dbContext.Orders.ToListAsync()));
        }

        [HttpPost]
        [Route("/order")]
        public async Task<IActionResult> AddOrder(OrderState state, OrderDeliveryMethod deliveryMethod, DateTime deliveryDate, OrderPaymentMethod paymentMethod, string transactionDetails, string transactionComments)
        {
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                PurchaseDate = DateTime.Now,
                State = state,
                DeliveryMethod = deliveryMethod,
                DeliveryDate = deliveryDate,
                PaymentMethod = paymentMethod,
                TransactionDetails = transactionDetails,
                TransactionCommnets = transactionComments,
                Products = new List<Product>()
            };
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPut]
        [Route("/OrderUpadateById/{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid orderId, OrderState state, OrderDeliveryMethod deliveryMethod, DateTime deliveryDate, OrderPaymentMethod paymentMethod, string transactionDetails, string transactionComments)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.State = state;
                order.DeliveryMethod = deliveryMethod;
                order.DeliveryDate = deliveryDate;
                order.PaymentMethod = paymentMethod;
                order.TransactionDetails = transactionDetails;
                order.TransactionCommnets = transactionComments;
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<OrderDTO>(order));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/OrderDeleteById/{orderId:guid}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] Guid orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("/orderItem")]
        public async Task<IActionResult> AddOrderItem(Guid orderId, Guid productId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            var product = await dbContext.Products.FindAsync(productId);
            if (order != null && product != null)
            {
                if (order.Products == null)
                {
                    order.Products = new List<Product>();
                }
                order.Products.Add(product);
                await dbContext.SaveChangesAsync();
                return Ok(_mapper.Map<OrderDTO>(order));
            }
            return NotFound();
        }

    }


}