using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using M183_Blog.Models;

namespace M183_Blog.Helpers
{
    public static class SessionHelper
    {
        private static HttpSessionState Session => HttpContext.Current.Session;

        public static List<string> Claims => (List<string>) Session["Claims"];
        public static User User => (User) Session["User"];

        public static string UserName => (string) Session["UserName"];

        public static void SetUser(User user)
        {
            Session["User"] = user;
            Session["Claims"] = user.Claims;
            Session["UserName"] = user.Username;
        }

        public static void ResetSession()
        {
            Session["User"] = null;
            Session["Claims"] = null;
            Session["UserName"] = null;
        }
    }
}