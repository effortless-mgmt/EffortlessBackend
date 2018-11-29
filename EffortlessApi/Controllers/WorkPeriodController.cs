using EffortlessApi.Core;
using EffortlessApi.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EffortlessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPeriodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkPeriodController(EffortlessContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }



    }
}