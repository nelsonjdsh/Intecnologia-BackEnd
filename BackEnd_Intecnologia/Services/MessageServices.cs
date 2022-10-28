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
                context.Database.ExecuteSqlInterpolated($"[dbo].[PCRSendMessage] { SendMessageEntity.IdSender}, {SendMessageEntity.IdReciever }, { SendMessageEntity.DescripcionMessage}, { Identity} out");
                result.Result = (int)Identity.Value;

            }
            catch (Exception ex)
            {
                result.Errors.Add(String.Format("Estimado usuario, en estos momentos estamos presentando inconvenientes en el servicio, favor comunicarse con un administrador."));
            }
            return result;
        }
    }
}
