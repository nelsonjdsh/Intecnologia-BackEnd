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
        [Column("FKIdUser")]
        public int FkidUser { get; set; }
        [Column("FKIdStand")]
        public int FkidStand { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateUserStand { get; set; }
    }
}
