using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class Vwactivity
    {
        [Column("PKIdActivity")]
        public int PkidActivity { get; set; }
        public string? DescriptionActivity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateActivity { get; set; }
        [StringLength(100)]
        public string? ScheduleActivity { get; set; }
        public int? PlaceActivity { get; set; }
        [StringLength(50)]
        public string? OrganizerActivity { get; set; }
        [StringLength(100)]
        public string? StartTimeActivity { get; set; }
        [StringLength(100)]
        public string? FinishTimeActivity { get; set; }
        [StringLength(250)]
        public string? DescriptionPlaceActivity { get; set; }
    }
}
