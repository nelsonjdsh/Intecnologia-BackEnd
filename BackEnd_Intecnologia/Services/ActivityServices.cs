using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using XAct;

namespace BackEnd_Intecnologia.Services
{
    public interface IActivityServices
    {
        Response<Vwactivity> GetActivity();
		Response<VwuserActivity> AssignActivity(AssignActivityEntity AssignActivityEntity);
		Response<VwactivityPlaceUser> GetActivityByPlaceUser(int IdUser, int IdPlace);
	}
    public class ActivityServices : IActivityServices
    {
        #region Entity Referencing Context
        private readonly IntecContext Context;
        #endregion
        public ActivityServices(IntecContext _Context)
        {
            Context = _Context;
        }
        public Response<Vwactivity> GetActivity()
        {
            var Response = new Response<Vwactivity>();

            try
            {
                var Data = Context.Vwactivities.FromSqlRaw("[dbo].[PRCGetActivity] {0},{1}", 0, 0).ToList();
                Response.DataList = Data;
            }
            catch(Exception ex)
            {
                Response.Errors.Add(ex.Message);
            }
            return Response;
        }
		
		public Response<VwuserActivity> AssignActivity(AssignActivityEntity AssignActivityEntity)
		{
			var result = new Response<VwuserActivity>();
			try
			{
		
				var Identity = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
				Context.Database.ExecuteSqlInterpolated($"[dbo].[PRCAssignActivity] {AssignActivityEntity.FkidUser}, {AssignActivityEntity.FkidActivity}, {Identity} out");
				var Data = Context.VwuserActivities.FromSqlRaw("[dbo].[PRCGetActivityByUser] {0}", AssignActivityEntity.FkidUser).ToList();
				var Progress = new SqlParameter("@Progress", SqlDbType.Int) { Direction = ParameterDirection.Output };
				Context.Database.ExecuteSqlInterpolated($"[dbo].[PRCGetProgressActivity] {AssignActivityEntity.FkidUser}, {Progress} out");
				result.Result = (int)Identity.Value;

				result.Progress = (int)Progress.Value;
				result.DataList = Data;
			}

			catch (Exception ex)
			{
				result.Errors.Add(String.Format("Estimado usuario, en estos momentos estamos presentando inconvenientes en el servicio, favor comunicarse con un administrador."));
			}
			return result;
		}
		
		public Response<VwactivityPlaceUser> GetActivityByPlaceUser (int IdUser, int IdPlace)
        {
			var Response = new Response<VwactivityPlaceUser>();
            try
            {
				var Data = Context.VwactivityPlaceUsers.FromSqlRaw($"[dbo].[PRCGetActivityByPlaceUser] {IdUser}, {IdPlace}").ToList();
				Response.DataList = Data;
            }
            catch (Exception ex)
            {
				Response.Errors.Add(ex.Message);
            }
			return Response;
        }
	}
}
