using M183_Blog.Helpers;
using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.Repositories
{
    public class CommentRepository : Repository
    {
        public CommentRepository(DataContext db) : base(db) { }

        public void AddComment(int postId, string text)
        {
            //ToDo: fix
            this.db.Comments.Add(new Comment() { User = SessionHelper.User, Commet = text, PostId = postId });
            db.SaveChanges();
        }
    }
}