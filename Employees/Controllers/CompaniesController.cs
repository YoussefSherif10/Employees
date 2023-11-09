using Employees.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetCompanies() =>
            Ok(await _service.Company.GetAllCompanies(false));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompany(int id) =>
            Ok(await _service.Company.GetCompanyById(id, false));
    }
}
