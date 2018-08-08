namespace TBT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserTimeLimit : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Users SET TimeLimit = 0 WHERE TimeLimit IS NULL");
            AlterColumn("dbo.Users", "TimeLimit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TimeLimit", c => c.Int());
        }
    }
}
