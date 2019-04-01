using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class UserAuthenticationContext : IdentityDbContext
    {
        public UserAuthenticationContext(DbContextOptions<UserAuthenticationContext> options)
            : base(options)
        {

        }
    }
}
