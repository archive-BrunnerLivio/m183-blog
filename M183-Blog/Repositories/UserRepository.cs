using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.Repositories
{
    public class UserRepository : Repository
    {
        public UserRepository(DataContext db) : base(db) { }

        public User GetUserById(int id)
        {
            return this.db.Users.First(u => u.Id == id);
        }
    }
}