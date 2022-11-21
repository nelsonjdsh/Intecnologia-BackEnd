﻿using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Intecnologia.Controllers
{
	[EnableCors("ReglasCors")]
	[Route("api/activity")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IActivityServices _activityServices;

		public ActivityController(IActivityServices activityServices)
		{
			_activityServices = activityServices;
		}

		[HttpGet]
		[Route("activitybyuser")]
		public ActionResult Get(int IdUser)
		{
			var result = _activityServices.ActivityByUser(IdUser);
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
		[Route("activity")]
		public ActionResult Get()
		{
			var result = _activityServices.GetActivity();
			if (result.DataList.Count() == 0)
			{
				return StatusCode(StatusCodes.Status404NotFound, new { result }); ;
			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result }); ;
			}


		}
		[HttpPost]
		[Authorize]
		[Route("assignactivity")]
		public ActionResult Post(AssignActivityEntity AssignActivityEntity)
		{
			var result = _activityServices.AssignActivity(AssignActivityEntity);
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
		[Route("activitybyplace")]
		public ActionResult GetByPlace(int IdLugar)
		{
			var result = _activityServices.GetActivityByPlace(IdLugar);
			if (result.DataList.Count() == 0)
			{
				return StatusCode(StatusCodes.Status404NotFound, new { result }); ;
			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result }); ;
			}
		}

		[HttpPost]
		[Route("activitybyplaceuser")]
		public ActionResult ActivityByPlaceUser(UserActivityPlaceEntity userActivityPlaceEntity)
		{
			var result = _activityServices.GetActivityByPlaceUser(userActivityPlaceEntity);
			if (result.DataList == null)
            {
				return StatusCode(StatusCodes.Status400BadRequest, new { result });
            }
			else
            {
				return StatusCode(StatusCodes.Status200OK, new { result });
            }

		}
	}
}
