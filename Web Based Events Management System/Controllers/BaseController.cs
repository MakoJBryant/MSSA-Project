using System.Web.Mvc;
using Events.Data;

namespace Events.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
    }
}