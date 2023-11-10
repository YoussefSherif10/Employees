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
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            var created = _service.Company.CreateCompany(company);
            return CreatedAtRoute("GetCompanyById", new { id = created.CompanyId }, created);
        }
    }
}
