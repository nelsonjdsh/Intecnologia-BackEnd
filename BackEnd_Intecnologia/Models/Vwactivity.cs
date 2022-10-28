﻿using System;
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
        [StringLength(100)]
        public string DescriptionActivity { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime CreationDateActivity { get; set; }
        [StringLength(100)]
        public string ScheduleActivity { get; set; } = null!;
    }
}