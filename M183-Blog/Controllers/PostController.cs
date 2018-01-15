using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.ViewModels;

namespace M183_Blog.Controllers
{
    public class PostController : Controller
    {
        public DataContext db = new DataContext();

        public ActionResult Index(int postId)
        {
            return View("Index",
                new DetailedPostViewModel(db.Posts.FirstOrDefault(p =>
                    p.Id == postId && (p.Status != PostStatus.Private || p.User == SessionHelper.User))));
        }
    }
}