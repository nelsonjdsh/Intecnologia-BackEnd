using BackEnd_Intecnologia.DTO;
using BackEnd_Intecnologia.Helpers;
using BackEnd_Intecnologia.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using XSystem.Security.Cryptography;

namespace BackEnd_Intecnologia.Services
{
	public interface IUserServices
	{
		//Response<ViewUser> Get(string Email, string Password);
		Response<Vwuser> SignIn(Login login);
		Response SignUP(UserEntity UserEntity);
		//Response Delete(int Id);
		Response<Vwuser> GetUser(int idUser);
	}
	public class UserServices : IUserServices
	{
		#region Entity Referencing Context
		private readonly IntecContext Context;

        #endregion
        public UserServices(IntecContext _Context)
		{
			Context = _Context;
        }

		public static string sha256(string input)
		{
			var crypt = new SHA256Managed();
			string Resulthash = String.Empty;
			byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(input));
			foreach (byte item in crypto)
			{
				Resulthash += item.ToString("x2");
			}
			return Resulthash;
		}
		public Response<Vwuser> SignIn(Login login)
		{

			var Result2 = new Response<Vwuser>();
			try
			{
				var Identity = new SqlParameter("@Message", SqlDbType.Int) { Direction = ParameterDirection.Output };
				string pass = sha256(login.Password);
				Context.Database.ExecuteSqlInterpolated($"[dbo].[PRCSignIn] {login.Email.ToLower()}, {pass},{Identity} out");
				int IdUser = (int)Identity.Value;
				if (IdUser != 0)
				{
					var Data = Context.Vwusers.FromSqlRaw("[dbo].[PRCUserData] {0}", IdUser).ToList();
					Result2.DataList = Data;
					Result2.Identity = IdUser;
					
				}
				else
				{
					Result2.StringCode = "Credenciales incorrectas";
				}

				return Result2;

			}
			catch (Exception ex)
			{
				string Mensaje = ex.Message;
				Result2.Errors.Add(string.Format("Estimado usuario, en estos momentos estamos presentando inconvenientes en el servicio, favor comunicarse con un administrador."));
				return Result2;
			}

		}
		static bool IsValidEmail(string email)
		{
			if (email == null)
			{
				return false;
			}
			if (new EmailAddressAttribute().IsValid(email))
			{
				return true;
			}
			else
			{

				return false;
			}
		}
		public Response SignUP(UserEntity UserEntity)
		{

			var Result2 = new Response();
			try
			{
				bool EmailCorrecto = IsValidEmail(UserEntity.EmailUser);
				if (UserEntity.EmailUser.ToLower() == "example@gmail.com")
				{
					EmailCorrecto = false;
				}
				if (EmailCorrecto)
				{
					var Identity = new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output };
					string pass = sha256(UserEntity.PasswordUser);
					Context.Database.ExecuteSqlInterpolated($"[dbo].[PCRSignUp] {UserEntity.EmailUser.ToLower()}, {UserEntity.FullNameUser},{UserEntity.IduserType},{pass},{Identity} out");
					Result2.Identity = (int)Identity.Value;
					if ((int)Identity.Value == 0)
					{
						Result2.StringCode = "Este correo ya existe";
					}
					else
					{
						Result2.StringCode = "Correo valido";

					}
			
				}
				else
				{
					Result2.Identity = 0;
					Result2.StringCode = "Correo invalido";
				}



			}
			catch (Exception ex)
			{
				string Mensaje = ex.Message;
				Result2.Errors.Add(string.Format("Estimado usuario, en estos momentos estamos presentando inconvenientes en el servicio, favor comunicarse con un administrador."));
			}
			return Result2;
		}

        public Response<Vwuser> GetUser(int idUser)
        {
            var Result = new Response<Vwuser>();
            try
            {
				var Data = Context.Vwusers.FromSqlRaw("[dbo].[PCRUserData] {0}", idUser).ToList();
				Result.DataList = Data;
			}
            catch (Exception _)
            {
				Result.Errors.Add(string.Format("ID no existe"));
            }
			return Result;
        }
    }
}
