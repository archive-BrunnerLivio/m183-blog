using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.Repositories;
using M183_Blog.ViewModels;

namespace M183_Blog.Controllers
{
    public class PostController : Controller
    {
        public DataContext db = new DataContext();
        private PostRepository postRepository;
        private CommentRepository commentRepository;

        public ActionResult Index(int postId)
        {
            this.postRepository = new PostRepository(db);
            return View("Index",
                new DetailedPostViewModel(this.postRepository.GetPublicPostById(postId)));
        }
        public ActionResult Comment(string comment, int postId) {

            this.postRepository = new PostRepository(db);
            this.commentRepository = new CommentRepository(db);
            if (SessionHelper.User != null)
            {
                commentRepository.AddComment(postId, comment);
            }
            return View("Index",
                new DetailedPostViewModel(this.postRepository.GetPublicPostById(postId)));
        }
    }
}