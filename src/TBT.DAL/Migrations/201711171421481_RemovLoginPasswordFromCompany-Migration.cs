namespace TBT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovLoginPasswordFromCompanyMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "Login");
            DropColumn("dbo.Companies", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Password", c => c.String(nullable: false, maxLength: 512));
            AddColumn("dbo.Companies", "Login", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
