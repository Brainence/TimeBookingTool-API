namespace TBT.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Company : DbMigration
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
            
            AddColumn("dbo.Customers", "CompanyId", c => c.Int());
            AddColumn("dbo.Users", "CompanyId", c => c.Int());
            CreateIndex("dbo.Customers", "CompanyId");
            CreateIndex("dbo.Users", "CompanyId");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id");
            AddForeignKey("dbo.Customers", "CompanyId", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Customers", new[] { "CompanyId" });
            DropIndex("dbo.Companies", "Company_CompanyName_Index");
            DropColumn("dbo.Users", "CompanyId");
            DropColumn("dbo.Customers", "CompanyId");
            DropTable("dbo.Companies");
        }
    }
}
