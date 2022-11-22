using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Helpers;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BackEnd_Intecnologia.Controllers
{
	[EnableCors("ReglasCors")]
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly IUserServices _IUserServices;
		private readonly JWTService _JWTService;

		#region Constructor
		public UserController(IUserServices service, JWTService JWTService)
		{
			_IUserServices = service;
			_JWTService = JWTService;
		}
		#endregion
		// GET: api/<UserController>
		[HttpPost]
		[Route("login")]
		public ActionResult Post(Login login)
		{
			var result = _IUserServices.SignIn(login);
			var jwt = _JWTService.Generate((int)result.Identity);
			Response.Cookies.Append("jwt", jwt, new CookieOptions
			{
				HttpOnly = true
			});

			if (result.DataList == null)
            {
				result.jwtToken = "";
				return StatusCode(StatusCodes.Status401Unauthorized, new { result });
            }
			else
            {
				result.jwtToken = jwt;
				return StatusCode(StatusCodes.Status200OK, new { result });
            }

		}

		[HttpPost]
		[Route("register")]
		public ActionResult SignUp(UserEntity UserEntity)
		{
			var result = _IUserServices.SignUP(UserEntity);
			if (result.StringCode == "Este correo ya existe")
			{
				return StatusCode(StatusCodes.Status200OK, new { result }); ;

			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result }); ;
			}
		}

		[HttpGet]
		[Route("authenticateuser")]
		public IActionResult AuthenticateUser()
        {
            try
            {
				var jwt = Request.Cookies["jwt"];
				var token = _JWTService.Verify(jwt);
				int userId = int.Parse(token.Issuer);
				var result = _IUserServices.GetUser(userId);
				return Ok(result);
            }
            catch (Exception)
            {
				return Unauthorized();
            }
        }

		[HttpPost]
        [Route("logout")]
		public IActionResult Logout()
        {
			Response.Cookies.Delete("jwt");
			return Ok(new
			{
				message = "Logged Out"
			});
        }


	}
}
