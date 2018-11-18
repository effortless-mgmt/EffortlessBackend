using System.Threading.Tasks;
using EffortlessApi.Core;
using EffortlessApi.Core.Models;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(EffortlessContext context) => _unitOfWork = new UnitOfWork(context);

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();

            if (companies == null) return NotFound();

            return Ok(companies);
        }
        [HttpGet("{companyId}", Name = "GetCompany")]
        public async Task<IActionResult> GetById(long companyId)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(companyId);

            if (company == null)
            {
                return NotFound($"Company {companyId} could not be found.");
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Company company)
        {
            var existingCompany = await _unitOfWork.Companies.GetByIdAsync(company.Id);

            if (existingCompany != null) return Ok(company);

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();

            return CreatedAtRoute("GetCompany", new { companyId = company.Id }, company);
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> Put(long companyId, Company company)
        {
            var existingCompany = await _unitOfWork.Companies.GetByIdAsync(companyId);

            if (existingCompany == null) return NotFound($"Company {companyId} could not be found.");

            await _unitOfWork.Companies.UpdateAsync(companyId, company);
            await _unitOfWork.CompleteAsync();

            return Ok(existingCompany);
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> Delete(long companyId)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(companyId);

            if (company == null)
            {
                return NoContent();
            }

            _unitOfWork.Companies.Remove(company);

            await _unitOfWork.CompleteAsync();

            return Ok(company);
        }
    }
}