using System.Web.Mvc;
using M183_Blog.Helpers;

namespace M183_Blog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [ClaimsAuthorize(Claims.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}