using Employees.Controllers.ActionFilters;
using Employees.Interfaces;
using Employees.Models.DTO;
using Employees.Models.Params;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/{v:apiversion}/companies/{companyId:int}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetEmployees")]
        [HttpHead]
        public async Task<IActionResult> Get(
            int companyId,
            [FromQuery] EmployeeParams employeeParams
        )
        {
            var (employeeDtos, paginginfoDto) = await _service
                .Employee
                .GetAllEmployees(companyId, employeeParams, false);
            Response.Headers.Add("X-Pagination", paginginfoDto.ToString());
            return Ok(employeeDtos);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployee(int companyId, int id) =>
            Ok(await _service.Employee.GetEmployeeById(companyId, id, false));

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateEmployee(int companyId, EmployeeForCreationDto employee)
        {
            var created = _service.Employee.CreateEmployee(companyId, employee);
            return CreatedAtRoute(
                "GetEmployeeById",
                new { companyId, id = created.EmployeeId },
                created
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int companyId, int id)
        {
            await _service.Employee.DeleteEmployee(companyId, id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployee(
            int companyId,
            int id,
            [FromBody] EmployeeForUpdateDto employee
        )
        {
            await _service.Employee.UpdateEmployee(companyId, id, employee);
            return NoContent();
        }
    }
}
