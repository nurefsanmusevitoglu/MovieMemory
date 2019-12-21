using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMemory.Identity
{
    public class IdentityMMContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityMMContext() : base("dataConnection")
        {
            //Database.SetInitializer<IdentityMMContext>(new IdentityMMInitializer());
        }
    }
}