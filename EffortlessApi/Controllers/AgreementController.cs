using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AgreementController(EffortlessContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var agreementModels = await _unitOfWork.Agreements.GetAllAsync();
            if (agreementModels == null) return NotFound("Could not find any agreements.");

            var agreementDTOs = _mapper.Map<List<AgreementDTO>>(agreementModels);

            return Ok(agreementDTOs.OrderBy(a => a.Id));
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetAgreement")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var agreementModel = await _unitOfWork.Agreements.GetByIdAsync(id);
            if (agreementModel == null) return NotFound($"Agreement with id {id} could not be found.");

            var agreementDTO = _mapper.Map<AgreementDTO>(agreementModel);

            return Ok(agreementDTO);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AgreementDTO agreementDTO)
        {
            if (agreementDTO == null) return BadRequest();

            var agreementModel = _mapper.Map<Agreement>(agreementDTO);
            await _unitOfWork.Agreements.AddAsync(agreementModel);
            await _unitOfWork.CompleteAsync();
            agreementDTO = _mapper.Map<AgreementDTO>(agreementModel);

            return CreatedAtRoute("GetAgreement", new { id = agreementDTO.Id }, agreementDTO);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, AgreementDTO agreementDTO)
        {
            var existing = await _unitOfWork.Agreements.GetByIdAsync(id);
            if (existing == null) return NotFound($"Agreement {id} could not be found.");

            var workPeriods = await _unitOfWork.WorkPeriods.FindAsync(w => w.AgreementId == id);
            if (workPeriods.ToList().Count != 0) return BadRequest("Can not change agreement as one or more work periods depend on it. Please create a new one.");

            var agreementModel = _mapper.Map<Agreement>(agreementDTO);
            await _unitOfWork.Agreements.UpdateAsync(id, agreementModel);
            await _unitOfWork.CompleteAsync();

            agreementDTO = _mapper.Map<AgreementDTO>(existing);

            return Ok(agreementDTO);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var agreementModel = await _unitOfWork.Agreements.GetByIdAsync(id);
            if (agreementModel == null) return NoContent();

            var workPeriods = await _unitOfWork.WorkPeriods.FindAsync(w => w.AgreementId == id);
            if (workPeriods.ToList().Count != 0) return BadRequest("Can not delete agreement as one or more work periods depend on it.");

            _unitOfWork.Agreements.Remove(agreementModel);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}