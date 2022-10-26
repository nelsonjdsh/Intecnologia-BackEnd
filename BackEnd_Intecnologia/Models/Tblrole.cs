using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLRole")]
    public partial class Tblrole
    {
        public Tblrole()
        {
            Tblusers = new HashSet<Tbluser>();
        }

        [Key]
        public int IdRole { get; set; }
        [StringLength(500)]
        public string? DescriptionRole { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateRole { get; set; }

        [InverseProperty("IdRoleNavigation")]
        public virtual ICollection<Tbluser> Tblusers { get; set; }
    }
}
