using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class VwuserStand
    {
        [Column("PKIdStand")]
        public int PkidStand { get; set; }
        [StringLength(500)]
        public string? DescriptionStand { get; set; }
        public int? IdStandType { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Visited { get; set; } = null!;
    }
}
