namespace TBT.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 512),
            //            IsActive = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "Customer_Name_Index");
            
            //CreateTable(
            //    "dbo.Projects",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 512),
            //            IsActive = c.Boolean(nullable: false),
            //            CustomerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
            //    .Index(t => t.Name, name: "Project_Name_Index")
            //    .Index(t => t.CustomerId);
            
            //CreateTable(
            //    "dbo.Activities",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 512),
            //            IsActive = c.Boolean(nullable: false),
            //            ProjectId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
            //    .Index(t => t.Name, name: "Activity_Name_Index")
            //    .Index(t => t.ProjectId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 512),
            //            LastName = c.String(nullable: false, maxLength: 512),
            //            Username = c.String(nullable: false, maxLength: 512),
            //            Password = c.String(nullable: false, maxLength: 512),
            //            IsAdmin = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            TimeLimit = c.Int(),
            //            CurrentTimeZone = c.Time(precision: 7),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Username, unique: true, name: "User_Username_Index");
            
            //CreateTable(
            //    "dbo.TimeEntries",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            ActivityId = c.Int(nullable: false),
            //            Duration = c.Time(nullable: false, precision: 7),
            //            Date = c.DateTime(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            IsRunning = c.Boolean(nullable: false),
            //            Comment = c.String(maxLength: 2048),
            //            TimeLimit = c.DateTime(),
            //            LastUpdated = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.ActivityId);
            
            //CreateTable(
            //    "dbo.ResetTickets",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Username = c.String(nullable: false, maxLength: 512),
            //            Token = c.String(nullable: false, maxLength: 64),
            //            ExpirationDate = c.DateTime(nullable: false),
            //            IsUsed = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserProject",
            //    c => new
            //        {
            //            ProjectId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ProjectId, t.UserId })
            //    .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.ProjectId)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserProject", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProject", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TimeEntries", "UserId", "dbo.Users");
            DropForeignKey("dbo.TimeEntries", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Activities", "ProjectId", "dbo.Projects");
            DropIndex("dbo.UserProject", new[] { "UserId" });
            DropIndex("dbo.UserProject", new[] { "ProjectId" });
            DropIndex("dbo.TimeEntries", new[] { "ActivityId" });
            DropIndex("dbo.TimeEntries", new[] { "UserId" });
            DropIndex("dbo.Users", "User_Username_Index");
            DropIndex("dbo.Activities", new[] { "ProjectId" });
            DropIndex("dbo.Activities", "Activity_Name_Index");
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Projects", "Project_Name_Index");
            DropIndex("dbo.Customers", "Customer_Name_Index");
            DropTable("dbo.UserProject");
            DropTable("dbo.ResetTickets");
            DropTable("dbo.TimeEntries");
            DropTable("dbo.Users");
            DropTable("dbo.Activities");
            DropTable("dbo.Projects");
            DropTable("dbo.Customers");
        }
    }
}
