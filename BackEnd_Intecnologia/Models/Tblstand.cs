using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLStand")]
    public partial class Tblstand
    {
        public Tblstand()
        {
            TbluserStands = new HashSet<TbluserStand>();
        }

        [Key]
        [Column("PKIdStand")]
        public int PkidStand { get; set; }
        [StringLength(500)]
        public string? DescriptionStand { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateStand { get; set; }
        public int? IdStandType { get; set; }
        public bool? Stand { get; set; }

        [ForeignKey("IdStandType")]
        [InverseProperty("Tblstands")]
        public virtual TblstandType? IdStandTypeNavigation { get; set; }
        [InverseProperty("FkidStandNavigation")]
        public virtual ICollection<TbluserStand> TbluserStands { get; set; }
    }
}
