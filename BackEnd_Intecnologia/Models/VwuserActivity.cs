using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class VwuserActivity
    {
        [Column("PKIdActivity")]
        public int PkidActivity { get; set; }
        public string? DescriptionActivity { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Visited { get; set; } = null!;
    }
}
