using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLMessage")]
    public partial class Tblmessage
    {
        [Key]
        [Column("PKIdMessage")]
        public int PkidMessage { get; set; }
        public int? IdSender { get; set; }
        public int? IdReciever { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDateMessage { get; set; }

        [ForeignKey("IdReciever")]
        [InverseProperty("TblmessageIdRecieverNavigations")]
        public virtual Tbluser? IdRecieverNavigation { get; set; }
        [ForeignKey("IdSender")]
        [InverseProperty("TblmessageIdSenderNavigations")]
        public virtual Tbluser? IdSenderNavigation { get; set; }
    }
}
