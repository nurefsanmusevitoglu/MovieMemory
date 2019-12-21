using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMemory.Identity
{
    public class IdentityMMInitializer : CreateDatabaseIfNotExists<IdentityMMContext>
    {
        protected override void Seed(IdentityMMContext context)
        {
            //Roles:
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "admin role" };
                manager.Create(role);
            }
            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "user role" };
                manager.Create(role);
            }

            //Users:
            if (!context.Users.Any(i => i.Name == "nmusevitoglu"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Nurefşan", Surname = "Müsevitoğlu", UserName = "nmusevitoglu", Email = "musevitoglunurefsan@gmail.com" };
                manager.Create(user, "1111");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.Name == "ynsengun"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Yusuf Nevzat", Surname = "Şengün", UserName = "ynsengun", Email = "yusuf.nevzat.sengun@gmail.com" };
                manager.Create(user, "mymmpass");
                manager.AddToRole(user.Id, "user");
            }

            base.Seed(context);
        }
    }
}