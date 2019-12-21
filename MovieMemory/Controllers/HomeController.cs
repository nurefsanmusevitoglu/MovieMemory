using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MovieMemory.Models;

namespace MovieMemory.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private MMContext db = new MMContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        public ActionResult Genres()
        {
            return PartialView(db.Genres.ToList());
        }

        public ActionResult MovieLists()
        {
            return PartialView(db.MovieLists.ToList());
        }

        public ActionResult MovieInfo(int id)
        {
            Movie m = db.Movies.Include("Genres").Include("Ratings").Include("Reviews").FirstOrDefault(i => i.Id == id);
            Console.WriteLine("Genre Count: {0}, Rating Count: {1}, Review Count: {2}", m.Genres.Count, m.Ratings.Count, m.Reviews.Count);
            int stars = 0;
            Rating r = db.Ratings.FirstOrDefault(i => i.UserId == db.Users.FirstOrDefault(a => a.Username == User.Identity.Name).Id && i.MovieId == id);
            if (r != null)
            {
                stars = r.Star;
            }
            ViewBag.stars = stars;
            ViewBag._users = db.Users.ToList();
            return View(m);
        }

        [HttpPost]
        public ActionResult SearchMovie(string searchedMovie)
        {
            var filteredMovies = new List<Movie>();
            foreach (var m in db.Movies.ToList())
            {
                if(m.Title.ToLower().Contains(searchedMovie.ToLower()))
                {
                    filteredMovies.Add(m);
                }
            }
            return View(filteredMovies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "Email, Password")]User u)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(x => x.Email == u.Email && x.Password == u.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ViewBag.InvalidUserErrorMessage = "*Invalid Email Address or Password!";
                    return View("Index", u);
                }
            }
            return View("Index", u);
        }

        [HttpPost]
        public ActionResult SignUp([Bind(Include = "Name, Surname, Email, Password")] User u, string Repassword)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == u.Email);
            if (user != null)
            {
                ViewBag.registeredEmailErrorMessage = "*This email has already been registered to the system!";
                return View("Index", u);
            }
            else if (ModelState.IsValid)
            {
                if (u.Password == Repassword)
                {
                    u.Role = "U";
                    u.Picture = "avatar.png";
                    u.Birthday = new DateTime(2000, 1, 1);
                    u.JoinTime = DateTime.Now;
                    u.PersonalMovieLists.Add(new PersonalMovieList() { Title = "Want-to-watch" });
                    u.PersonalMovieLists.Add(new PersonalMovieList() { Title = "Watched" });
                    db.Users.Add(u);
                    db.SaveChanges();
                    u.Username = "user" + u.Id;
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ViewBag.passwordErrorMessage = "*Passwords do not match!";
                }
            }
            return View("Index", u);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}