namespace TBT.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddCompanyApproachMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 512),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true, name: "Company_CompanyName_Index");
            
            AddColumn("dbo.Customers", "CompanyId", c => c.Int(nullable: true));
            AddColumn("dbo.Users", "CompanyId", c => c.Int(nullable: true));
            CreateIndex("dbo.Customers", "CompanyId");
            CreateIndex("dbo.Users", "CompanyId");
            AddForeignKey("dbo.Customers", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id");
            Sql($"INSERT INTO dbo.Companies(CompanyName, IsActive) VALUES ('Brainence', 1) UPDATE dbo.Users SET CompanyId = 1 WHERE CompanyId is NULL UPDATE dbo.Customers SET CompanyId = 1 WHERE CompanyId is NULL");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            DropIndex("dbo.Companies", "Company_CompanyName_Index");
            DropColumn("dbo.Users", "CompanyId");
            DropColumn("dbo.Customers", "CompanyId");
            DropTable("dbo.Companies");
        }
    }
}
