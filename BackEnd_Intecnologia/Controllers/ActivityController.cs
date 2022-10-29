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
    public class ActivityController : ControllerBase
    {
        private readonly IActivityServices _activityServices;

        public ActivityController(IActivityServices activityServices)
        {
            _activityServices = activityServices;
        }

        [HttpGet]
        [Route("Activity")]
        public ActionResult Get()
        {
            var result = _activityServices.GetActivity();
            return StatusCode(StatusCodes.Status200OK, new { result }); ;
        }
    }
}
