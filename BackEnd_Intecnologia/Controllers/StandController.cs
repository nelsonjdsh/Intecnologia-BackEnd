using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Intecnologia.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class StandController : ControllerBase
    {
        private readonly IAssignStandServices _IAssignStandServices;

        public StandController(IAssignStandServices service)
        {
            _IAssignStandServices = service;
        }

        [HttpPost]
        [Route("AssignStand")]
        public ActionResult Post(AssignStandEntity AssignStandEntity)
        {
            var result = _IAssignStandServices.AssignStand(AssignStandEntity);
            return StatusCode(StatusCodes.Status200OK, new { result });
        }
    }
}
