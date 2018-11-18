using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var departments = await _unitOfWork.Department.GetAllAsync();

            if (departments == null) return NotFound();

            return Ok(departments);
        }

        [HttpGet("{departmentId}", Name = "Getdepartment")]
        public async Task<IActionResult> GetById(long departmentId)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound($"department {departmentId} could not be found.");
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Department department)
        {
            var existingdepartment = await _unitOfWork.Department.GetByIdAsync(department.Id);

            if (existingdepartment != null) return Ok(department);

            await _unitOfWork.Department.AddAsync(department);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("Getdepartment", new { departmentId = department.Id }, department);
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> Put(long departmentId, Department department)
        {
            var existingdepartment = await _unitOfWork.Department.GetByIdAsync(departmentId);

            if (existingdepartment == null) return NotFound($"department {departmentId} could not be found.");

            await _unitOfWork.Department.UpdateAsync(departmentId, department);
            await _unitOfWork.CompleteAsync();

            return Ok(existingdepartment);
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> Delete(long departmentId)
        {
            var department = await _unitOfWork.Department.GetByIdAsync(departmentId);

            if (department == null)
            {
                return NoContent();
            }

            _unitOfWork.Department.Remove(department);

            await _unitOfWork.CompleteAsync();

            return Ok(department);
        }
    }
}