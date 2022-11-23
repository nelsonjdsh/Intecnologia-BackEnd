using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class Agendum
    {
        [StringLength(255)]
        public string? Día { get; set; }
        [StringLength(255)]
        public string? Hora { get; set; }
        public string? Actividad { get; set; }
        [StringLength(255)]
        public string? Lugar { get; set; }
        [StringLength(255)]
        public string? Organiza { get; set; }
        [StringLength(255)]
        public string Puntuable { get; set; } = null!;
    }
}
