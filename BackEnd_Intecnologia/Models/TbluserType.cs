using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLUserType")]
    public partial class TbluserType
    {
        public TbluserType()
        {
            Tblusers = new HashSet<Tbluser>();
        }

        [Key]
        [Column("PKIdUserType")]
        public int PkidUserType { get; set; }
        [StringLength(500)]
        public string? DescriptionUserType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateUserType { get; set; }

        [InverseProperty("IduserTypeNavigation")]
        public virtual ICollection<Tbluser> Tblusers { get; set; }
    }
}
