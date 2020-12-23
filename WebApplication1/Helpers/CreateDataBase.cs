using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Models;

namespace AuthProject.Helpers
{
    public static class CreateDataBase
    {
        public static void Initialize(ProjectContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        FirstName = "iPhone X",
                        LastName = "Apple",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
