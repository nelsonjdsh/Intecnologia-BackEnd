﻿using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Intecnologia.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
	[Authorize]
	[ApiController]
    public class StandController : ControllerBase
    {
        private readonly IStandServices _IStandServices;

        public StandController(IStandServices service)
        {
            _IStandServices = service;
        }

        [HttpPost]
        [Route("AssignStand")]
        public ActionResult Post(AssignStandEntity AssignStandEntity)
        {
            var result = _IStandServices.AssignStand(AssignStandEntity);
			if (result.Result == 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { result });
		
			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result });
			}
           
        }

        [HttpGet]
        [Route("StandByUser")]
        public ActionResult Get(int IdUser)
        {
            var result = _IStandServices.StandByUser(IdUser);
            if (result.StringCode == "No hay registro con este Id")
            {
				return StatusCode(StatusCodes.Status204NoContent, new { result });
			}
            else
            {
				return StatusCode(StatusCodes.Status200OK, new { result });
			}
      
        }

		[HttpGet]
		[Route("GetStands")]
		public ActionResult GetStands()
		{
			var result = _IStandServices.GetStands();
			if (result.StringCode == "No hay registros")
			{
				return StatusCode(StatusCodes.Status204NoContent, new { result });
			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result });
			}
		}
		[HttpGet]
		[Route("GetStandsById")]
		public ActionResult GetStandsById(int Id)
		{
			var result = _IStandServices.GetStandsById(Id);
			if (result.StringCode == "No hay registro con este Id")
			{
				return StatusCode(StatusCodes.Status204NoContent, new { result });
			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result });
			}
		}
	}
}
