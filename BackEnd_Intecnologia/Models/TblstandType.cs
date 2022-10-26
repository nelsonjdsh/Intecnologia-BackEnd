using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLStandType")]
    public partial class TblstandType
    {
        public TblstandType()
        {
            Tblstands = new HashSet<Tblstand>();
        }

        [Key]
        public int IdStandType { get; set; }
        [StringLength(500)]
        public string DescriptionStandType { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime CreationDateStandType { get; set; }

        [InverseProperty("IdStandTypeNavigation")]
        public virtual ICollection<Tblstand> Tblstands { get; set; }
    }
}
