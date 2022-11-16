using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BackEnd_Intecnologia.Services
{
    public interface IStandServices
    {
        Response AssignStand(AssignStandEntity AssignStandEntity);
        Response<VwuserStand> StandByUser(int IdUser);
		Response<Vwstand> GetStands();
		Response<Vwstand> GetStandsById(int Id);
	}

    public class StandServices : IStandServices
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

        public Response<VwuserStand> StandByUser(int IdUser)
        {
            var result = new Response<VwuserStand>();

            try
            {
                var Data = context.VwuserStands.FromSqlRaw("[dbo].[GetStandsByUser] {0}", IdUser).ToList();
				var Progress = new SqlParameter("@Progress", SqlDbType.Int) { Direction = ParameterDirection.Output };
				context.Database.ExecuteSqlInterpolated($"[dbo].[PRCGetProgress] {IdUser}, {Progress} out");
				result.Progress = (int)Progress.Value;
				if (Data.Count == 0)
				{
					result.DataList = Data;
					result.StringCode = "No hay registro con este Id";
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
		public Response<Vwstand> GetStands()
		{
			var result = new Response<Vwstand>();

			try
			{
				var Data = context.Vwstands.FromSqlRaw("[dbo].[PRCGetStand] {0},{1}",0,0).ToList();
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

		public Response<Vwstand> GetStandsById(int Id)
		{
			var result = new Response<Vwstand>();

            try
            {
                var Data = context.Vwstands.FromSqlRaw("[dbo].[PRCGetStand] {0},{1}", Id, 1).ToList();
                if (Data.Count == 0)
                {
					result.DataList = Data;
                    result.StringCode = "No hay registro con este Id";
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
