using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLPlaceActivity")]
    public partial class TblplaceActivity
    {
        [Key]
        [Column("PKIdPlaceActivity")]
        public int PkidPlaceActivity { get; set; }
        [StringLength(250)]
        public string? DescriptionPlaceActivity { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreationDatePlaceActivity { get; set; }
    }
}
