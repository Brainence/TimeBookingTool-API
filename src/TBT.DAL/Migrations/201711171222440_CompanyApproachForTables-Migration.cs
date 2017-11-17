namespace TBT.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;

    public partial class CompanyApproachForTablesMigration : DbMigration
    {
        public override void Up()
        {
            Sql($"INSERT INTO Companies(Login, Password, CompanyName, IsActive) VALUES ('schuiko@brainence.com', '{new PasswordHasher().HashPassword("brainence!")}', 'Brainence', 1)");

            AddColumn("dbo.Customers", "CompanyId", c => c.Int(nullable: true));
            AddColumn("dbo.Projects", "CompanyId", c => c.Int(nullable: true));
            AddColumn("dbo.Users", "CompanyId", c => c.Int(nullable: true));
            CreateIndex("dbo.Customers", "CompanyId");
            CreateIndex("dbo.Projects", "CompanyId");
            CreateIndex("dbo.Users", "CompanyId");
            AddForeignKey("dbo.Customers", "CompanyId", "dbo.Companies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Projects", "CompanyId", "dbo.Companies", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Companies WHERE CompanyName = 'Brainence'");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.Customers", new[] { "CompanyId" });

            DropColumn("dbo.Users", "CompanyId");
            DropColumn("dbo.Projects", "CompanyId");
            DropColumn("dbo.Customers", "CompanyId");
        }
    }
}
