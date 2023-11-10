using Employees.Interfaces;
using Employees.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

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

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> Get(int companyId) =>
            Ok(await _service.Employee.GetAllEmployees(companyId, false));

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployee(int companyId, int id) =>
            Ok(await _service.Employee.GetEmployeeById(companyId, id, false));

        [HttpPost]
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
