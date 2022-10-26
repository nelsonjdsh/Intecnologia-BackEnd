using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLUserStand")]
    public partial class TbluserStand
    {
        [Key]
        [Column("FKIdUser")]
        public int FkidUser { get; set; }
        [Key]
        [Column("FKIdStand")]
        public int FkidStand { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateUserStand { get; set; }

        [ForeignKey("FkidStand")]
        [InverseProperty("TbluserStands")]
        public virtual Tblstand FkidStandNavigation { get; set; } = null!;
        [ForeignKey("FkidUser")]
        [InverseProperty("TbluserStands")]
        public virtual Tbluser FkidUserNavigation { get; set; } = null!;
    }
}
