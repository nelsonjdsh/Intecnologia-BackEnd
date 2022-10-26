using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_Intecnologia.Models
{
    [Table("TBLUser")]
    public partial class Tbluser
    {
        public Tbluser()
        {
            TblmessageIdRecieverNavigations = new HashSet<Tblmessage>();
            TblmessageIdSenderNavigations = new HashSet<Tblmessage>();
            TbluserStands = new HashSet<TbluserStand>();
        }

        [Key]
        [Column("PKIdUser")]
        public int PkidUser { get; set; }
        [StringLength(100)]
        public string EmailUser { get; set; } = null!;
        [StringLength(100)]
        public string FullNameUser { get; set; } = null!;
        public int IdRole { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreationDateUser { get; set; }
        [StringLength(16)]
        public string PasswordUser { get; set; } = null!;
        [Column("IDUserType")]
        public int IduserType { get; set; }

        [ForeignKey("IdRole")]
        [InverseProperty("Tblusers")]
        public virtual Tblrole IdRoleNavigation { get; set; } = null!;
        [ForeignKey("IduserType")]
        [InverseProperty("Tblusers")]
        public virtual TbluserType IduserTypeNavigation { get; set; } = null!;
        [InverseProperty("IdRecieverNavigation")]
        public virtual ICollection<Tblmessage> TblmessageIdRecieverNavigations { get; set; }
        [InverseProperty("IdSenderNavigation")]
        public virtual ICollection<Tblmessage> TblmessageIdSenderNavigations { get; set; }
        [InverseProperty("FkidUserNavigation")]
        public virtual ICollection<TbluserStand> TbluserStands { get; set; }
    }
}
