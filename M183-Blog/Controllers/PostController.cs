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
        private PostRepository postRepository;

        public ActionResult Index(int postId)
        {
            this.postRepository = new PostRepository(db);
            return View("Index",
                new DetailedPostViewModel(this.postRepository.GetPublicPostById(postId, SessionHelper.User)));
        }
    }
}