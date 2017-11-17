namespace TBT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferencesToCompanyForTables : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Users SET CompanyId = 1 WHERE CompanyId is NULL");
            Sql("UPDATE dbo.Projects SET CompanyId = 1 WHERE CompanyId is NULL");
            Sql("UPDATE dbo.Customers SET CompanyId = 1 WHERE CompanyId is NULL");
        }
        
        public override void Down()
        {
            Sql("UPDATE dbo.Users SET CompanyId = 1 WHERE CompanyId is NULL");
            Sql("UPDATE dbo.Projects SET CompanyId = 1 WHERE CompanyId is NULL");
            Sql("UPDATE dbo.Customers SET CompanyId = 1 WHERE CompanyId is NULL");
        }
    }
}
