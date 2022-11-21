using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace BackEnd_Intecnologia.Services
{
    public interface IMessageServices
    {
        Response SendMessage(SendMessageEntity SendMessageEntity);
		Response<Vwmessage> GetMessages();
	}
    public class MessageServices : IMessageServices
    {
        private readonly IntecContext context;

        public MessageServices(IntecContext _context)
        {
            context = _context;
        }

        public Response SendMessage(SendMessageEntity SendMessageEntity)
        {
            var result = new Response();
            try
            {
                var Identity = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                context.Database.ExecuteSqlInterpolated($"[dbo].[PRCSendMessage] { SendMessageEntity.IdSender}, { SendMessageEntity.DescripcionMessage}, { Identity} out");
                result.Identity = (int)Identity.Value;

            }
            catch (Exception ex)
            {
                result.Errors.Add(String.Format("Estimado usuario, en estos momentos estamos presentando inconvenientes en el servicio, favor comunicarse con un administrador."));
            }
            return result;
        }
		public Response<Vwmessage> GetMessages()
		{
			var result = new Response<Vwmessage>();

			try
			{
				var Data = context.Vwmessages.FromSqlRaw("[dbo].[PRCGetMessage]" ).ToList();
				if (Data.Count == 0)
				{
					result.DataList = Data;
					result.StringCode = "No hay registros";
					result.Result = 0;
				}
				else
				{
					result.DataList = Data;
					result.Result = 1;
				}
			}
			catch (Exception ex)
			{
				result.Errors.Add(ex.Message);
			}
			return result;
		}
	}
}
