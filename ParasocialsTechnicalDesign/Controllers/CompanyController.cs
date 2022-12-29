using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParasocialsPOSAPI.Data;
using ParasocialsPOSAPI.Models;

namespace ParasocialsPOSAPI.Controllers
{
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ParasocialsPOSAPIDbContext dbContext;

        public CompanyController(ParasocialsPOSAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("/getCompanies")]
        public async Task<IActionResult> GetCompanyList()
        {
            return Ok(await dbContext.Companies.ToListAsync());
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
            return Ok(await dbContext.Companies.Where(c => c.ServiceType == serviceType).ToListAsync());
        }


        [HttpGet]
        [Route("/getCompanyByName/{companyName}")]
        public async Task<IActionResult> GetCompanyByName([FromRoute] string companyName)
        {
            var company = await dbContext.Companies.Where(c => c.CompanyName == companyName).FirstOrDefaultAsync();
            if (company != null)
            {
                return Ok(company);
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
                return Ok(company);
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
        [Route("/AddCompany")]
        public async Task<IActionResult> AddCompany(AddCompany addCompany)
        {
            var company = new Company()
            {
                SupplierId = Guid.NewGuid(),
                CompanyName = addCompany.CompanyName,
                ServiceType = addCompany.ServiceType,
                Address = addCompany.Address,
                ContactInformation = addCompany.ContactInformation,
                RelationshipType = addCompany.RelationshipType
            };

            await dbContext.Companies.AddAsync(company);
            await dbContext.SaveChangesAsync();

            return Ok(company);
        }
    }





}