using System.Collections.Generic;
using System.Linq;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.Repositories;

namespace M183_Blog
{
    public class PostRepository : Repository
    {
        public void Init()
        {
        }

        public PostRepository(DataContext db)
            : base(db)
        {
        }

        public IEnumerable<Post> GetPosts()
        {
            return this.db.Posts.ToList();
        }

        public IEnumerable<Post> GetPublicPosts()
        {
            return this.db.Posts.Where(p => p.Status == PostStatus.Public).ToList();
        }

        public IEnumerable<Post> GetPrivatePosts()
        {
            return this.db.Posts.Where(p => p.Status == PostStatus.Private).ToList();
        }

        public Post GetPublicPostById(int postId)
        {
            return this.db.Posts.FirstOrDefault(p =>
                      p.Id == postId && (p.Status != PostStatus.Private || p.User == SessionHelper.User));
        }
    }
}