using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WcfService2
{
    public class AppDBContext : DbContext
    {

        public AppDBContext():base("TestDBContext")
        {
            

            Database.SetInitializer<AppDBContext>(new MigrateDatabaseToLatestVersion<AppDBContext,
                    Configuration>());
        }
            public DbSet<User> Users { get; set; }
       

    }

    public partial class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string userrole { get; set; }
        public int enabled { get; set; }

    }

    internal sealed class Configuration : DbMigrationsConfiguration<AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDBContext context)
        {
            context.Users.AddOrUpdate(i => i.id,
                new User
                {
                    id=1,
                    username = "test user",
                    password="pin1",
                    Email="test@email.com",
                    Phone="12345678",
                    userrole="",
                    enabled=1
                }
           );

        }

    }
}