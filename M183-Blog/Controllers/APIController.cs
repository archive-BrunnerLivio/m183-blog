using M183_Blog.Dtos;
using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M183_Blog.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public JsonResult Posts()
        {
            DataContext db = new DataContext();
            List<PostDto> postDtos = new List<PostDto>();
            IEnumerable<Post> dbPosts = new PostRepository(db).GetPublicPosts();

            foreach (Post post in dbPosts)
            {
                PostDto postDto = new PostDto()
                {
                    Content = post.Content,
                    Description = post.Description,
                    Id = post.Id,
                    Title = post.Title
                };
                postDtos.Add(postDto);
            }

            return Json(postDtos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Post(int id)
        {
            DataContext db = new DataContext();
            Post post = new PostRepository(db).GetPublicPostById(id);

            PostDto postDto = new PostDto()
            {
                Content = post.Content,
                Description = post.Description,
                Id = post.Id,
                Title = post.Title
            };

            return Json(postDto, JsonRequestBehavior.AllowGet);
        }
    }
}