using System;
using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.Repositories;
using M183_Blog.Services;
using M183_Blog.ViewModels;

namespace M183_Blog.Controllers
{
    public class LoginController : Controller
    {
        public DataContext db = new DataContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) //ToDo: Umschriibe das es nümme kopiert wirkt
            {
                if (db.Users.Any(x => x.Username == model.Username))
                {
                    User user = db.Users.First(x => x.Username == model.Username);
                    if (PasswordHash.Verify(model.Password, user.Password))
                    {
                        Token token = new Token()
                        {
                            Expiry = DateTime.Now.AddMinutes(5),
                            TokenNr = new Random().Next(1, 999999),
                            UserId = user.Id,
                            User = user
                        };
                        db.Tokens.Add(token);
                        db.SaveChanges();
                        new NexmoService().SendSMS(token.TokenNr, user.Mobilephonenumber);
                        ViewBag.Status = "sms_sent";
                        return
                            View("TokenLogin",
                                new TokenViewModel()
                                {
                                    UserId = user.Id
                                });
                    }

                    LogUserAction("Wrong password", user.Id);
                    ModelState.AddModelError("Password", "The password is wrong.");
                    return View("Index", model);
                }
                else
                {
                    LogUserAction("Login failed", null);
                    ModelState.AddModelError("Username", "There is no User with this Username.");
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult TokenLogin(TokenViewModel model)
        {
            // Create repos
            TokenRepository tokenRepository = new TokenRepository(db);
            UserRepository userRepository = new UserRepository(db);

            User user = userRepository.GetUserById(model.UserId);

            if (tokenRepository.VerifyToken(model.Token, model.UserId))
            {
                LogUserAction("Login successful", model.UserId);
                SessionHelper.SetUser(user);

                if (SessionHelper.HasClaim(Claims.Admin))
                {
                    return RedirectToAction("Index", "Admin");
                } else if(SessionHelper.HasClaim(Claims.Create))
                {
                    return RedirectToAction("Index", "User");
                }

                return RedirectToAction("Index", "Home");
            }

            LogUserAction("Invalid token", model.UserId);
            ViewBag.Status = "invalid_token";
            ModelState.AddModelError("Token", "Token is not valid");
            return View("TokenLogin", model);
        }

        private void LogUserAction(string action, int? userId)
        {
            db.UserLogs.Add(new UserLog(action, db.Users.FirstOrDefault(x => x.Id.Equals(userId.Value))));
            db.SaveChanges();
        }
    }
}