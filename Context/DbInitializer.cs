using AngularDotNet.Models;
using System;
using System.Linq;

namespace AngularDotNet.Context
{
    public static class DbInitializer
    {
        public static void Initialize(AngularDotNetContext context)
        {
            context.Database.EnsureCreated();

            // Look for any appusers.
            if (context.AppUsers.Any())
            {
                return;   // DB has been seeded
            }

            var users = new AppUser[]
            {
            new AppUser{ID=Guid.NewGuid().ToString(), FirstName="Carson",LastName="Daily", Email="", SignUpDate=DateTime.Parse("2021-09-01")},
            new AppUser{ID=Guid.NewGuid().ToString(), FirstName="Hulk",LastName="Hogan", Email="", SignUpDate=DateTime.Parse("2021-11-01")}  
            };

            foreach (AppUser s in users)
            {
                context.AppUsers.Add(s);
            }
            
            context.SaveChanges();
        }
    }
}