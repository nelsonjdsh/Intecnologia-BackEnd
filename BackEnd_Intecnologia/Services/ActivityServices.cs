using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Services
{
    public interface IActivityServices
    {
        Response<Vwactivity> GetActivity();
		Response<Vwactivity> GetActivityByPlace(int IdLugar);
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
	}
}
