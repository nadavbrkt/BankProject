using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LidavBankProj.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LidavBankProj.Models
{
    [Table("OfficeModel")]
    public class OfficeModel
    {
        [Key]
        [Display(Name="Office ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Office Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Office City")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Office Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Office House Number")]
        public int House { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Office Opening Hours")]
        public string OpeningHours { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Office Closing Hours")]
        public string ClosingHours { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Office Phone NUmber")]
        public string Phone { get; set; }

        [InverseProperty("OfficeModel")]
        public virtual ICollection<AccountModel> User { get; set; }
    }

}