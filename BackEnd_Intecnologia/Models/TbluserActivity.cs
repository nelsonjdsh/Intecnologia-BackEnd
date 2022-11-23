using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLUserActivity")]
    public partial class TbluserActivity
    {
        [Key]
        [Column("PKUserActivity")]
        public long PkuserActivity { get; set; }
        [Column("FKIdActivity")]
        public int? FkidActivity { get; set; }
        [Column("FKIdUser")]
        public int? FkidUser { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }
    }
}
