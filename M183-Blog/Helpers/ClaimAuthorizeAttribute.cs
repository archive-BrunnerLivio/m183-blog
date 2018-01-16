using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace M183_Blog.Helpers
{
    public enum ClaimCheckOperator
    {
        All,
        Any
    }

    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public ClaimsAuthorizeAttribute(ClaimCheckOperator checkOperator, params string[] claims)
        {
            CheckOperator = checkOperator;
            Claims = new HashSet<string>(claims, StringComparer.InvariantCultureIgnoreCase);
        }

        public ClaimsAuthorizeAttribute(params string[] claims)
            : this(ClaimCheckOperator.All, claims)
        {
        }

        public ClaimCheckOperator CheckOperator { get; set; }

        /// <summary>
        ///     Hide unused property
        /// </summary>
        private new string Roles { get; set; }

        /// <summary>
        ///     Hide unused property
        /// </summary>
        private new string Users { get; set; }

        public ISet<string> Claims { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return SessionHelper.Claims != null && SessionHelper.Claims.Any(x => Claims.Contains(x));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var redirectToUrl = VirtualPathUtility.ToAbsolute("~/Error/AccessDenied");
                filterContext.Result = filterContext.HttpContext.Request.ContentType == "application/json"
                    ? (ActionResult)
                    new JsonResult
                    {
                        Data = new {RedirectTo = redirectToUrl},
                        ContentEncoding = Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.DenyGet
                    }
                    : new ContentResult
                    {
                        Content = redirectToUrl,
                        ContentEncoding = Encoding.UTF8,
                        ContentType = "text/html"
                    };

                //Important: Cannot set 401 as asp.net intercepts and returns login page
                //so instead set 530 User access denied            
                filterContext.HttpContext.Response.StatusCode = 530; //User Access Denied
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                return;
            }
            filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(new {controller = "Error", action = "AccessDenied"}));
        }
    }
}