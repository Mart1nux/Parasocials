using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;
using AutoMapper;
using ParasocialsPOSAPI.Data_Transfer_Objects;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;
        private readonly IMapper _mapper;


        public CompanyController(ParasocialsPOSAPIDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("/getCompanies")]
        public async Task<IActionResult> GetCompanyList()
        {
            return Ok(_mapper.Map<List<CompanyDTO>>(await dbContext.Companies.ToListAsync()));
        }

        [HttpGet]
        [Route("/getCompaniesByRelationshipType/{relationshipType}")]
        public async Task<IActionResult> GetCompanyByRelationshipType([FromRoute] CompanyRelationshipType relationshipType)
        {
            return Ok(await dbContext.Companies.Where(c => c.RelationshipType == relationshipType).ToListAsync());
        }

        [HttpGet]
        [Route("/getCompaniesByServiceType/{serviceType}")]
        public async Task<IActionResult> GetCompanyByServiceType([FromRoute] CompanyServiceType serviceType)
        {
            return Ok(_mapper.Map<List<CompanyDTO>>(await dbContext.Companies.Where(c => c.ServiceType == serviceType).ToListAsync()));
        }


        [HttpGet]
        [Route("/getCompanyByName/{companyName}")]
        public async Task<IActionResult> GetCompanyByName([FromRoute] string companyName)
        {
            var company = await dbContext.Companies.Where(c => c.CompanyName == companyName).FirstOrDefaultAsync();
            if (company != null)
            {
                return Ok(_mapper.Map<CompanyDTO>(company));
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/getCompaniesBySupplierId/{supplierId:guid}")]
        public async Task<IActionResult> GetCompanyBySupplier([FromRoute] Guid supplierId)
        {
            var company = await dbContext.Companies.FindAsync(supplierId);
            if (company != null)
            {
                return Ok(_mapper.Map<CompanyDTO>(company));
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("/deleteACompanyByCompanyName/{companyName}")]
        public async Task<IActionResult> DeleteACompanyByCompanyName([FromRoute] string companyName)
        {
            var company = await dbContext.Companies.Where(c => c.CompanyName == companyName).FirstOrDefaultAsync();
            if (company != null)
            {
                dbContext.Companies.Remove(company);
                await dbContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        [HttpPost]
        [Route("/AddCompany/{companyName}")]
        public async Task<IActionResult> AddCompany([FromRoute] string companyName, CompanyServiceType serviceType, string address, string contactInformation, CompanyRelationshipType relationshipType)
        {
            var company = new Company()
            {
                SupplierId = Guid.NewGuid(),
                CompanyName = companyName,
                ServiceType = serviceType,
                Address = address,
                ContactInformation = companyName,
                RelationshipType = relationshipType
            };

            await dbContext.Companies.AddAsync(company);
            await dbContext.SaveChangesAsync();

            return Ok(_mapper.Map<CompanyDTO>(company));
        }
    }





}