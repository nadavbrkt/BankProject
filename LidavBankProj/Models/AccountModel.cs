using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LidavBankProj.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LidavBankProj.Models
{
    [Table("AccountModel")]
    public class AccountModel
    {

        [Key]
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Account Holder")]
        public string AccountHolder { get; set; }

        [Required]
        [Display(Name = "Current Amount")]
        public int CurrentAmount { get; set; }

        [Required]
        [Display(Name = "Office ID")]
        public int OfficeID { get; set; }

        [InverseProperty("SrcAccount")]
        public virtual ICollection<TransactionModel> SrcTransactions { get; set;}

        [InverseProperty("DstAccount")]
        public virtual ICollection<TransactionModel> DstTransactions { get; set; }

        public virtual OfficeModel OfficeModel { get; set; }

        [ForeignKey("AccountHolder")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        /*
        [ForeignKey("AccountHolder")]
        public virtual ApplicationUser User { get; set; }
        */
    }
}