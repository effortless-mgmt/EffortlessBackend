using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using EffortlessLibrary.DTO;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var agreementModels = await _unitOfWork.Agreements.GetAllAsync();
            if (agreementModels == null) return NotFound();

            var agreementDTOs = _mapper.Map<List<AgreementDTO>>(agreementModels);

            return Ok(agreementDTOs);
        }

        [HttpGet("{id}", Name = "GetAgreement")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var agreementModel = await _unitOfWork.Agreements.GetByIdAsync(id);
            if (agreementModel == null) return NotFound($"Agreement with id {id} could not be found.");

            var agreementDTO = _mapper.Map<AgreementDTO>(agreementModel);

            return Ok(agreementDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AgreementDTO agreementDTO)
        {
            if (agreementDTO == null) return BadRequest();

            var agreementModel = _mapper.Map<Agreement>(agreementDTO);
            await _unitOfWork.Agreements.AddAsync(agreementModel);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetAgreement", new { id = agreementModel.Id }, agreementModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, AgreementDTO agreementDTO)
        {
            var existingAgreement = _unitOfWork.Agreements.GetByIdAsync(id);
            if (existingAgreement == null) return NotFound($"Agreement {id} could not be found.");

            var workPeriods = _unitOfWork.WorkPeriods.FindAsync(w => w.AgreementId == id);
            if (workPeriods != null) return BadRequest("Can not change agreement as one or more work periods depend on it. Please create a new one.");

            var agreementModel = _mapper.Map<Agreement>(agreementDTO);
            await _unitOfWork.Agreements.UpdateAsync(id, agreementModel);
            await _unitOfWork.CompleteAsync();

            return Ok(agreementDTO);
        }

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