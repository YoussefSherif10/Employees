using Employees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId:int}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int companyId) =>
            Ok(await _service.Employee.GetAllEmployees(companyId, false));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int companyId, int id) =>
            Ok(await _service.Employee.GetEmployeeById(companyId, id, false));
    }
}
