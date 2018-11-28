using System.Collections.Generic;
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
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(context);
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

        // @TODO: Check if any TWPs rely on the agreement. If so, DO NOT DELETE.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var agreement = await _unitOfWork.Agreements.GetByIdAsync(id);

            if (agreement == null) return NoContent();

            _unitOfWork.Agreements.Remove(agreement);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // @TODO: Check if any TWPs rely on the agreement. If so, DO NOT update.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateAsync(long id, AgreementDTO agreementDTO)
        // {
        //     var existingAgreement = _unitOfWork.Agreements.GetByIdAsync(id);
        //     if (existingAgreement == null) return NotFound($"Agreement {id} could not be found.");

        //     var temporaryWorkPeriods = _unitOfWork.
        // }

    }
}