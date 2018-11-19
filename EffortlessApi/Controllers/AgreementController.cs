using System.Threading.Tasks;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public AgreementController(EffortlessContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var agreements = await _unitOfWork.Agreements.GetAllAsync();

            if (agreements == null) return NotFound();

            return Ok(agreements);
        }

        [HttpGet("{id}", Name = "GetAgreement")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var agreement = await _unitOfWork.Agreements.GetByIdAsync(id);

            if (agreement == null) return NotFound($"Agreement with id {id} could not be found.");

            return Ok(agreement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Agreement agreement)
        {
            if (agreement == null) return BadRequest();

            await _unitOfWork.Agreements.AddAsync(agreement);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetAgreement", new { id = agreement.Id }, agreement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var agreement = await _unitOfWork.Agreements.GetByIdAsync(id);

            if (agreement == null) return NoContent();

            _unitOfWork.Agreements.Remove(agreement);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}