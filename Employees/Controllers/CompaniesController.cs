using Employees.Controllers.ModelBinders;
using Employees.Interfaces;
using Employees.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetCompanies")]
        public async Task<IActionResult> GetCompanies() =>
            Ok(await _service.Company.GetAllCompanies(false));

        [HttpGet("{id:int}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompany(int id) =>
            Ok(await _service.Company.GetCompanyById(id, false));

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var created = await _service.Company.CreateCompany(company);
            return CreatedAtRoute("GetCompanyById", new { id = created.CompanyId }, created);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids
        )
        {
            if (!ids.Any())
                return BadRequest("Empty list of ids");

            return Ok(await _service.Company.GetCompaniesByIds(ids, false));
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompaniesByIds(
            [FromBody] IEnumerable<CompanyForCreationDto> companies
        )
        {
            if (!companies.Any())
                return BadRequest("empty companies list");

            var (companyCollection, ids) = await _service
                .Company
                .CreateCompanyCollection(companies);
            System.Console.WriteLine(ids);
            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollection);
        }
    }
}
