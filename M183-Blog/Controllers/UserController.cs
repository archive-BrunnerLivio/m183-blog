using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.ViewModels;
using M183_Blog.Repositories;

namespace M183_Blog.Controllers
{
    public class UserController : Controller
    {
        public DataContext db = new DataContext();
        private UserRepository userRepository;

        [ClaimsAuthorize(Claims.Comment)]
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
    }
}