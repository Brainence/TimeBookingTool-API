namespace TBT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserTimeLimit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "TimeLimit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "TimeLimit", c => c.Int());
        }
    }
}
