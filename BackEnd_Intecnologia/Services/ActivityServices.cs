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
		Response<VwuserActivity> ActivityByUser(int IdUser);
		Response<Vwactivity> GetActivityByPlace(int IdLugar);
		Response<VwuserActivity> AssignActivity(AssignActivityEntity AssignActivityEntity);
		Response<VwactivityPlaceUser> GetActivityByPlaceUser(UserActivityPlaceEntity userActivityPlaceEntity);
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
		public Response<VwuserActivity> ActivityByUser(int IdUser)
		{
			var result = new Response<VwuserActivity>();

			try
			{
				var Data = Context.VwuserActivities.FromSqlRaw("[dbo].[PRCGetActivityByUser] {0}", IdUser).ToList();
				var Progress = new SqlParameter("@Progress", SqlDbType.Int) { Direction = ParameterDirection.Output };
				Context.Database.ExecuteSqlInterpolated($"[dbo].[PRCGetProgressActivity] {IdUser}, {Progress} out");
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
		public Response<Vwactivity> GetActivityByPlace(int IdLugar)
		{
			var Response = new Response<Vwactivity>();

			try
			{
				var Data = Context.Vwactivities.FromSqlRaw("[dbo].[PRCGetActivity] {0},{1}", IdLugar, 1).ToList();
				Response.DataList = Data;
			}
			catch (Exception ex)
			{
				Response.Errors.Add(ex.Message);
			}
			return Response;
		}

		public Response<VwactivityPlaceUser> GetActivityByPlaceUser (UserActivityPlaceEntity userActivityPlaceEntity)
        {
			var Response = new Response<VwactivityPlaceUser>();
            try
            {
				var Data = Context.VwactivityPlaceUsers.FromSqlRaw($"[dbo].[PRCGetActivityByPlaceUser] {userActivityPlaceEntity.IdUser}, {userActivityPlaceEntity.IdPlace}").ToList();
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
