namespace PokeGoTrade.Migrations.AccountMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PokeGoTrade.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\AccountMigrations";
        }

        protected override void Seed(PokeGoTrade.Models.ApplicationDbContext context)
        {
            //Initialize the managers/stores
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // If role does not exists, create it
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));
            }

            // Create test users
            //Create administrator test user
            var adminUser = userManager.FindByName("admin");
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    UserName = "admin",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = userManager.Create(newAdminUser, "P@ssw0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newAdminUser.Id, false);
                    userManager.AddToRole(newAdminUser.Id, "admin");
                }
            }

            // Create test users
            //Create administrator test user
            var test = userManager.FindByName("hpark77");
            if (test == null)
            {
                var newTest = new ApplicationUser()
                {
                    UserName = "hpark77",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = userManager.Create(newTest, "P@ssw0rd");
                if (result.Succeeded)
                {
                    userManager.SetLockoutEnabled(newTest.Id, false);
                    userManager.AddToRole(newTest.Id, "User");
                }
            }
        }
    }
}