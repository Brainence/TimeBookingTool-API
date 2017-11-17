namespace TBT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixReletionsMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Companies");
            AddForeignKey("dbo.Users", "CompanyId", "dbo.Companies", "Id");
        }
    }
}
