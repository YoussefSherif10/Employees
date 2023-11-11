using Employees.Controllers.ActionFilters;
using Employees.Controllers.ModelBinders;
using Employees.Interfaces;
using Employees.Models.DTO;
using Employees.Models.Params;
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
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParams companyParams)
        {
            var (companyDtos, pagingInfoDto) = await _service
                .Company
                .GetAllCompanies(companyParams, false);
            return Ok(new { Companies = companyDtos, PagingInfo = pagingInfoDto });
        }

        [HttpGet("{id:int}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompany(int id) =>
            Ok(await _service.Company.GetCompanyById(id, false));

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompaniesByIds(
            [FromBody] IEnumerable<CompanyForCreationDto> companies
        )
        {
            var (companyCollection, ids) = await _service
                .Company
                .CreateCompanyCollection(companies);
            return CreatedAtRoute("CompanyCollection", new { ids }, companyCollection);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _service.Company.DeleteCompany(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(
            int id,
            [FromBody] CompanyForUpdateDto company
        )
        {
            await _service.Company.UpdateCompany(id, company);
            return NoContent();
        }
    }
}
