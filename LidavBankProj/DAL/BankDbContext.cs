using LidavBankProj.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LidavBankProj.DAL
{
    public class BankDbContext: DbContext
    {
        public DbSet<AccountModel> AccountModel { get; set; }
        public DbSet<OfficeModel> OfficeModel { get; set; }
        public DbSet<TransactionModel> TransactionModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel >()
                .HasMany(t => t.SrcTransactions).WithRequired(t => t.SrcAccount)
                .HasForeignKey(t => t.SrcAccountID).WillCascadeOnDelete(false);
           
            modelBuilder.Entity<AccountModel>()
               .HasMany(t => t.DstTransactions).WithRequired(t => t.DstAccount)
                .HasForeignKey(t => t.DstAccountID).WillCascadeOnDelete(true);

            modelBuilder.Entity<OfficeModel>()
                .HasMany(t => t.User).WithRequired(t => t.OfficeModel)
                .HasForeignKey(t => t.OfficeID).WillCascadeOnDelete(false);
           
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(l=>l.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(l => new { l.RoleId, l.UserId });

            modelBuilder.Entity<IdentityUser>().HasKey(s => s.Id);
            /*
            modelBuilder.Entity<AccountModel>()
                .HasMany(t => t.Transactions).WithRequired(a => a.DstAccount)
                .WillCascadeOnDelete(false);

            


            modelBuilder.Entity<AccountModel>()
                .HasMany(t => t.Transactions).WithRequired(a => a.SrcAccount)
                .HasForeignKey(a => a.SrcAccountID).WillCascadeOnDelete(false);

            /*
            modelBuilder.Entity<TransactionModel>()
                        .HasRequired(t => t.SrcAccount)
                        .WithMany(t => t.Transactions)
                        .HasForeignKey(b => b.SrcAccountID)
                        .WillCascadeOnDelete(false); 
                        

              modelBuilder.Entity<TransactionModel>()
                        .HasRequired(t => t.DstAccount)
                        .WithMany(t => t.Transactions)
                        .HasForeignKey(b => b.DstAccountID)
                        .WillCascadeOnDelete(false);
             */
        }

    }
}