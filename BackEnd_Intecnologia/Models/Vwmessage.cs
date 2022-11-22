using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Keyless]
    public partial class Vwmessage
    {
        [Column("PKIdMessage")]
        public int PkidMessage { get; set; }
        public int? IdSender { get; set; }
        [Column(TypeName = "text")]
        public string? DescripcionMessage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateMessage { get; set; }
        public TimeSpan? TimeMessage { get; set; }
    }
}
