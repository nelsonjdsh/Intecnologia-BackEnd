using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class Vwuser
    {
        [Column("PKIdUser")]
        public int PkidUser { get; set; }
        [StringLength(100)]
        public string EmailUser { get; set; } = null!;
        [StringLength(100)]
        public string FullNameUser { get; set; } = null!;
        public int IdRole { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDateUser { get; set; }
        [StringLength(200)]
        public string PasswordUser { get; set; } = null!;
        [Column("IDUserType")]
        public int IduserType { get; set; }
    }
}
