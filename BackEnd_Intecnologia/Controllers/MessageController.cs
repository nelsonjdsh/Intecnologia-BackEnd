using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_Intecnologia.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageServices _IMessageServices;

        public MessageController(IMessageServices service)
        {
            _IMessageServices = service;
        }

        [HttpPost]
        [Route("sendmessage")]
        public ActionResult Post(SendMessageEntity SendMessageEntity)
        {
            var result = _IMessageServices.SendMessage(SendMessageEntity);
            return StatusCode(StatusCodes.Status200OK, new { result });
        }
		[HttpGet]
		[Route("getmessage")]
		public ActionResult GetMessage()
		{
			var result = _IMessageServices.GetMessages();
			if (result.StringCode == "No hay registros")
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
