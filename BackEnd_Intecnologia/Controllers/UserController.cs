using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Helpers;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BackEnd_Intecnologia.Controllers
{
	[EnableCors("ReglasCors")]
	[Route("api/[controller]")]
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
		[Route("Login")]
		public ActionResult Post(Login login)
		{
			var result = _IUserServices.SignIn(login);
            var jwt = _JWTService.Generate((int)result.Identity);
			Response.Cookies.Append("jwt", jwt, new CookieOptions
			{
				HttpOnly = true
			});
            result.jwtToken = "success";
            return StatusCode(StatusCodes.Status200OK, new { result });
        }

		[HttpPost]
		[Route("Register")]
		public ActionResult SignUp(UserEntity UserEntity)
		{
			var result = _IUserServices.SignUP(UserEntity);
			if (result.StringCode == "Este correo ya existe")
			{
				return StatusCode(StatusCodes.Status400BadRequest, new { result }); ;

			}
			else
			{
				return StatusCode(StatusCodes.Status200OK, new { result }); ;
			}
	
		}

	}
}
