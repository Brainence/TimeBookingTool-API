using Microsoft.AspNet.Identity;
using System.Data.Entity;
using TBT.DAL.Entities;
using TBT.DAL.Migrations;



namespace TBT.DAL.Repository
{
    public class DatabaseInitializer : MigrateDatabaseToLatestVersion<DataContext,Configuration>
    {
       
    }
}
