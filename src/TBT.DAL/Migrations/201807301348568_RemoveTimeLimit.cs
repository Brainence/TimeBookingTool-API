namespace TBT.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveTimeLimit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TimeEntries", "TimeLimit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeEntries", "TimeLimit", c => c.DateTime());
        }
    }
}
