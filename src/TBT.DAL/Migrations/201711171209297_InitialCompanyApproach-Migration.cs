namespace TBT.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCompanyApproachMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 512),
                        Password = c.String(nullable: false, maxLength: 512),
                        CompanyName = c.String(nullable: false, maxLength: 512),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true, name: "Company_CompanyName_Index");
        }
        
        public override void Down()
        {
            
            DropIndex("dbo.Companies", "Company_CompanyName_Index");
            DropTable("dbo.Companies");
        }
    }
}
