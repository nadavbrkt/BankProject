using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using LidavBankProj.Models;

namespace LidavBankProj.Models
{
    [Table("TransactionModel")]
    public class TransactionModel
    {
        [Key]
        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }
        
        [Required]     
        [Display(Name = "Source Account")]
        public int SrcAccountID { get; set; }
        

        [Required]
        [Display(Name = "Destination Account")]
        public int DstAccountID { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Transaction Time")]
        public DateTime Time { get; set; }
        
        [ForeignKey("DstAccountID")]
        public virtual AccountModel DstAccount { get; set; }

        [ForeignKey("SrcAccountID")]
        public virtual AccountModel SrcAccount { get; set; }
        
    }
}