using Employees.Controllers.ActionFilters;
using Employees.Controllers.ModelBinders;
using Employees.Interfaces;
using Employees.Models.DTO;
using Employees.Models.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/{v:apiversion}/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetCompanies")]
        [HttpHead]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyParams companyParams)
        {
            var (companyDtos, pagingInfoDto) = await _service
                .Company
                .GetAllCompanies(companyParams, false);

            Response.Headers.Add("X-Pagination", pagingInfoDto.ToString());
            return Ok(companyDtos);
        }

        [HttpGet("{id:int}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompany(
            int id,
            [FromQuery] CompanyParams companyParams
        ) => Ok(await _service.Company.GetCompanyById(id, companyParams, false));

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var created = await _service.Company.CreateCompany(company);
            return CreatedAtRoute("GetCompanyById", new { id = created.CompanyId }, created);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids,
            [FromQuery] CompanyParams companyParams
        )
        {
            if (!ids.Any())
                return BadRequest("Empty list of ids");

            return Ok(await _service.Company.GetCompaniesByIds(ids, companyParams, false));
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
