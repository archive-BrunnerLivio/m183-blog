using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}