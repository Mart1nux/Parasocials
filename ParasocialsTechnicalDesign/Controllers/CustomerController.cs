using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;


        public CustomerController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("/createACustomerByCustomerHimself/{name}")]
        public async Task<IActionResult> AddCustomer([FromRoute] string name, string surname, DateTime dateOfBirth, string phoneNumber, string emailAddress, string membershipCard, string username, string password)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = emailAddress,
                JoinDate = DateTime.Now,
                MembershipCard = membershipCard
            };

            var loyalty = new Loyalty()
            {
                LoyaltyId = Guid.NewGuid(),
                Customer = customer,
                LoyaltyDiscount = 0,
                Type = LoyaltyType.BronzeCustomer
            };

            customer.Loyalty = loyalty;
            customer.LoyaltyId = loyalty.LoyaltyId;

            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost]
        [Route("/createACustomerByEmployee/{name}")]
        public async Task<IActionResult> AddCustomerByEmployee([FromRoute] string name, string surname, DateTime dateOfBirth, string phoneNumber, string emailAddress, string membershipCard)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = emailAddress,
                JoinDate = DateTime.Now,
                MembershipCard = membershipCard
            };

            var loyalty = new Loyalty()
            {
                LoyaltyId = Guid.NewGuid(),
                Customer = customer,
                LoyaltyDiscount = 0,
                Type = LoyaltyType.BronzeCustomer
            };

            customer.Loyalty = loyalty;
            customer.LoyaltyId = loyalty.LoyaltyId;

            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }

        [HttpDelete]
        [Route("/deleteCustomerById/{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            var customer = await dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpPut]
        [Route("/cahngeCustomerInformationByHisName{name}")]
        public async Task<IActionResult> ChangeCustomerInformation([FromRoute] string name, string surname, DateTime dateOfBirth, string phoneNumber, string emailAddress, string password)
        {
            var customer = await dbContext.Customers.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.Name = name;
                customer.Surname = surname;
                customer.DateOfBirth = dateOfBirth;
                customer.PhoneNumber = phoneNumber;
                customer.Email = emailAddress;
                dbContext.SaveChanges();
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("/cahngeCustomerLoyaltyTypeByHisMembershipCard{membershipCard}")]
        public async Task<IActionResult> ChangeCustomerLoyaltyInformation([FromRoute] string membershipCard, LoyaltyType type)
        {
            var customer = await dbContext.Customers.Include(e => e.Loyalty).Where(c => c.MembershipCard == membershipCard).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.Loyalty.Type = type;
                dbContext.SaveChanges();
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCustomerById/{customerId:Guid}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid customerId)
        {
            var customer = await dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCustomerByMembershipCard/{membershipCard}")]
        public async Task<IActionResult> GetCustomerByMembershipCard([FromRoute] string membershipCard)
        {
            var customer = await dbContext.Customers.Where(c => c.MembershipCard == membershipCard).FirstOrDefaultAsync();
            if (customer != null)
            {
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCustomerByName/{name}")]
        public async Task<IActionResult> GetCustomerByName([FromRoute] string name)
        {
            var customer = await dbContext.Customers.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (customer != null)
            {
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCustomerBySurname/{surname}")]
        public async Task<IActionResult> GetCustomerBySurname([FromRoute] string surname)
        {
            var customer = await dbContext.Customers.Where(c => c.Surname == surname).FirstOrDefaultAsync();
            if (customer != null)
            {
                return Ok(_mapper.Map<CustomerDTO>(customer));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCustomers")]
        public async Task<IActionResult> GetCustomerList()
        {
            return Ok(_mapper.Map<List<CustomerDTO>>(await dbContext.Customers.ToListAsync()));
        }
    }
}
