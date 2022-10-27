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
	public class UserController : ControllerBase
	{

		private readonly IUserServices _IUserServices;

		#region Constructor
		public UserController(IUserServices service)
		{
			_IUserServices = service;
		}
		#endregion
		// GET: api/<UserController>
		[HttpPost]
		[Route("Login")]
		public ActionResult Post(Login login)
		{
			var result = _IUserServices.SignIn(login);
			return StatusCode(StatusCodes.Status200OK, new { result }); ;
		}

		[HttpPost]
		[Route("Register")]
		public ActionResult SignUp(UserEntity UserEntity)
		{
			var result = _IUserServices.SignUP(UserEntity);
			return StatusCode(StatusCodes.Status200OK, new { result }); ;
		}

	}
}
