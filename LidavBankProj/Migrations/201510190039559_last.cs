namespace LidavBankProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccountModel", "ApplicationUser_Id", "dbo.IdentityUsers");
            DropIndex("dbo.AccountModel", new[] { "AccountHolder" });
            DropIndex("dbo.AccountModel", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AccountModel", "AccountHolder");
            RenameColumn(table: "dbo.AccountModel", name: "ApplicationUser_Id", newName: "AccountHolder");
            AlterColumn("dbo.AccountModel", "AccountHolder", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AccountModel", "AccountHolder");
            AddForeignKey("dbo.AccountModel", "AccountHolder", "dbo.IdentityUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountModel", "AccountHolder", "dbo.IdentityUsers");
            DropIndex("dbo.AccountModel", new[] { "AccountHolder" });
            AlterColumn("dbo.AccountModel", "AccountHolder", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.AccountModel", name: "AccountHolder", newName: "ApplicationUser_Id");
            AddColumn("dbo.AccountModel", "AccountHolder", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AccountModel", "ApplicationUser_Id");
            CreateIndex("dbo.AccountModel", "AccountHolder");
            AddForeignKey("dbo.AccountModel", "ApplicationUser_Id", "dbo.IdentityUsers", "Id");
        }
    }
}
