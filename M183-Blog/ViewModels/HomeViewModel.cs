using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.ViewModels
{
    public class HomeViewModel
    {
        List<Post> posts;
        public HomeViewModel(List<Post> posts)
        {
            this.posts = posts;
        }

        public List<Post> Posts
        {
            get
            {
                return this.posts;
            }
        }
    }
}