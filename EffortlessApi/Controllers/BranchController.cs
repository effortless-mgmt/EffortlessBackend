using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Branches = await _unitOfWork.Branches.GetAllAsync();

            if (Branches == null) return NotFound();

            return Ok(Branches);
        }

        [HttpGet("{branchId}", Name = "GetBranch")]
        public async Task<IActionResult> GetById(long branchId)
        {
            var branch = await _unitOfWork.Branches.GetByIdAsync(branchId);

            if (branch == null)
            {
                return NotFound($"Branch {branchId} could not be found.");
            }

            return Ok(branch);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Branch branch)
        {
            var existingBranch = await _unitOfWork.Branches.GetByIdAsync(branch.Id);

            if (existingBranch != null) return Ok(branch);

            await _unitOfWork.Branches.AddAsync(branch);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetBranch", new { branchId = branch.Id }, branch);
        }

        [HttpPut("{branchId}")]
        public async Task<IActionResult> Put(long branchId, Branch branch)
        {
            var existingBranch = await _unitOfWork.Branches.GetByIdAsync(branchId);

            if (existingBranch == null) return NotFound($"Branch {branchId} could not be found.");

            await _unitOfWork.Branches.UpdateAsync(branchId, branch);
            await _unitOfWork.CompleteAsync();

            return Ok(existingBranch);
        }
        [HttpDelete("{branchId}")]
        public async Task<IActionResult> Delete(long branchId)
        {
            var branch = await _unitOfWork.Branches.GetByIdAsync(branchId);

            if (branch == null)
            {
                return NotFound($"Branch {branchId} could not be found.");
            }

            _unitOfWork.Branches.Remove(branch);

            await _unitOfWork.CompleteAsync();

            return Ok(branch);
        }
    }
}