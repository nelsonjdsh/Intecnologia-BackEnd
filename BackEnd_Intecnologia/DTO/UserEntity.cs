using BackEnd_Intecnologia.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd_Intecnologia.DTO
{
	public class UserEntity
	{
		public int PkidUser { get; set; }

		public string EmailUser { get; set; } = null!;

		public string FullNameUser { get; set; } = null!;
		public int IdRole { get; set; }

		public DateTime CreationDateUser { get; set; }
		public string PasswordUser { get; set; } = null!;

		public int IduserType { get; set; }

	}
}
