using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BackEnd_Intecnologia.Services
{
    public interface IAssignStandServices
    {
        Response AssignStand(AssignStandEntity AssignStandEntity);
    }

    public class StandServices : IAssignStandServices
    {
        private readonly IntecContext context;

        public StandServices(IntecContext _context)
        {
            context = _context;
        }

        public Response AssignStand(AssignStandEntity AssignStandEntity)
        {
            var result = new Response();
            try
            {
                var Identity = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
                context.Database.ExecuteSqlInterpolated($"[dbo].[PCRAssignStand] {AssignStandEntity.FkidUser}, {AssignStandEntity.FkidStand}, {Identity} out");
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
