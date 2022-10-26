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
        public int IdUser { get; set; }
        [Key]
        public int IdStand { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateUserStand { get; set; }

        [ForeignKey("IdStand")]
        [InverseProperty("TbluserStands")]
        public virtual Tblstand IdStandNavigation { get; set; } = null!;
        [ForeignKey("IdUser")]
        [InverseProperty("TbluserStands")]
        public virtual Tbluser IdUserNavigation { get; set; } = null!;
    }
}
