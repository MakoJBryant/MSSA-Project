using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Events.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // 5. Modify the ApplicationDbContext class to include the events and comments as IDbSet<T>:
        // Events and Comments get/set methods were originally public, but they were giving me error code CS0053.
        IDbSet<Event> Events { get; set; }

        IDbSet<Comment> Comments { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}