using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLActivity")]
    public partial class Tblactivity
    {
        [Key]
        public int IdActivity { get; set; }
        [StringLength(100)]
        public string DescriptionActivity { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime CreationDateActivity { get; set; }
        [StringLength(100)]
        public string ScheduleActivity { get; set; } = null!;
    }
}
