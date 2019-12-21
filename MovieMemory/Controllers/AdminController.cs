using MovieMemory.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMemory.Controllers
{
    [Authorize(Roles = "A")]
    public class AdminController : Controller
    {
        private MMContext db = new MMContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.countries = db.Countries.ToList();
            return View(db.Users.Where(i => i.Username != User.Identity.Name).ToList());
        }

        public ActionResult Movies()
        {
            return View(db.Movies.ToList());
        }

        public ActionResult MovieLists()
        {
            return View(db.MovieLists.ToList());
        }

        public ActionResult Genres()
        {
            return View(db.Genres.ToList());
        }

        /*
         * User operations
         */
        [HttpPost]
        public ActionResult SearchUser(string searchedUser)
        {
            var filteredUsers = new List<User>();
            //forun içini değiştir..........................................................................
            foreach (var u in db.Users.Where(i => i.Username != User.Identity.Name).ToList())
            {
                var uname = u.Name.ToLower() + " " + u.Surname.ToLower();
                if (uname.Contains(searchedUser.ToLower()))
                {
                    filteredUsers.Add(u);
                }
            }
            ViewBag.searchMessage = "There are " + filteredUsers.Count + " users found";
            return View("Users", filteredUsers);
        }
        public ActionResult UserDetail(int id)
        {
            List<User> friends = db.Friends.Where(i => i.UserId1 == id && i.IsFriend).Select(i => i.User2).ToList();
            List<User> friends2 = db.Friends.Where(i => i.UserId2 == id && i.IsFriend).Select(i => i.User1).ToList();
            friends.AddRange(friends2);
            ViewBag.friends = friends;

            db.PersonalMovieLists.Where(i => i.UserId == id).Include(i => i.Movies);
            return View(db.Users.Include(i => i.PersonalMovieLists).FirstOrDefault(i => i.Id == id));
        }
        public ActionResult UserDelete(int id)
        {
            User u = db.Users.Find(id);
            if(u != null && u.Role != "A")
            {
                var friends = db.Friends.Where(i => i.UserId1 == id || i.UserId2 == id).ToList();
                foreach (var friend in friends)
                {
                    db.Friends.Remove(friend);
                }
                db.Users.Remove(u);
                db.SaveChanges();
            }

            return RedirectToAction("Users");
        }
        public ActionResult AddToAdmin(int id)
        {
            db.Users.Find(id).Role = "A";
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        public ActionResult RemoveFromAdmin(int id)
        {
            if (db.Users.Find(id).Username != "nmusevitoglu")
            {
                db.Users.Find(id).Role = "U";
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }
        /*
         * Movie operations
         */
        [HttpPost]
        public ActionResult SearchMovie(string searchedMovie)
        {
            var filteredMovies = new List<Movie>();
            foreach (var m in db.Movies.ToList())
            {
                if (m.Title.ToLower().Contains(searchedMovie.ToLower()))
                {
                    filteredMovies.Add(m);
                }
            }
            ViewBag.searchMessage = "There are " + filteredMovies.Count + " movies found";
            return View("Movies", filteredMovies);
        }
        [HttpGet]
        public ActionResult MovieAdd()
        {
            ViewBag.countries = db.Countries.Select(i => i.CountryName).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MovieAdd(Movie m)
        {
            if (ModelState.IsValid && m.Duration > 0)
            {
                db.Movies.Add(m);
                db.SaveChanges();
                return RedirectToAction("Movies");
            }
            ViewBag.countries = db.Countries.Select(i => i.CountryName).ToList();
            return View(m);
        }
        [HttpGet]
        public ActionResult MovieEdit(int id)
        {
            ViewBag.countries = db.Countries.Select(i => i.CountryName).ToList();
            Movie m = db.Movies.Find(id);
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MovieEdit([Bind(Include = "Id, title, theme, director, country, duration, releaseDate, poster, rating" )] Movie m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Movies");
            }
            return View(m);
        }
        public ActionResult MovieDelete(int id)
        {
            Movie m = db.Movies.Find(id);
            if (m != null)
            {
                db.Movies.Remove(m);
                db.SaveChanges();
            }

            return RedirectToAction("Movies");
        }

        /*
         * Genre operations
         */
        [HttpPost]
        public ActionResult SearchGenre(string searchedGenre)
        {
            var filteredGenres = new List<Genre>();
            foreach (var g in db.Genres.ToList())
            {
                if (g.GenreName.ToLower().Contains(searchedGenre.ToLower()))
                {
                    filteredGenres.Add(g);
                }
            }
            ViewBag.searchMessage = "There are " + filteredGenres.Count + " genres found";
            return View("Genres", filteredGenres);
        }
        [HttpGet]
        public ActionResult GenreAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenreAdd([Bind(Include = "Id, genreName")] Genre g)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(g);
                db.SaveChanges();
                return RedirectToAction("Genres");
            }
            return View(g);
        }
        [HttpGet]
        public ActionResult GenreEdit(int id)
        {
            Genre g = db.Genres.Find(id);
            return View(g);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenreEdit([Bind(Include = "Id, genreName")] Genre g)
        {
            if (ModelState.IsValid)
            {
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Genres");
            }
            return View(g);
        }
        public ActionResult GenreDelete(int id)
        {
            Genre g = db.Genres.Find(id);
            if (g != null)
            {
                db.Genres.Remove(g);
                db.SaveChanges();
            }

            return RedirectToAction("Genres");
        }

        /*
         * Movie List operations
         */
        [HttpPost]
        public ActionResult SearchMovieList(string searchedMovieList)
        {
            var filteredMovieLists = new List<MovieList>();
            foreach (var ml in db.MovieLists.ToList())
            {
                if (ml.Title.ToLower().Contains(searchedMovieList.ToLower()))
                {
                    filteredMovieLists.Add(ml);
                }
            }
            ViewBag.searchMessage = "There are " + filteredMovieLists.Count + " movie lists found";
            return View("MovieLists", filteredMovieLists);
        }
        [HttpGet]
        public ActionResult MovieListAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MovieListAdd([Bind(Include = "Id, title")] MovieList ml)
        {
            if (ModelState.IsValid)
            {
                ml.Movies = new List<Movie>();
                db.MovieLists.Add(ml);
                db.SaveChanges();
                return RedirectToAction("MovieLists");
            }
            return View(ml);
        }
        public ActionResult AddMovieToMovieList(int movieId, int listId)
        {
            db.MovieLists.Find(listId).Movies.Add(db.Movies.Find(movieId));
            db.SaveChanges();
            return RedirectToAction("MovieListEdit", new { id = listId });
        }
        public ActionResult DeleteMovieFromMovieList(int movieId, int listId)
        {
            MovieList ml = db.MovieLists.Include(i => i.Movies).FirstOrDefault(i => i.Id == listId);
            Movie m = db.Movies.Find(movieId);
            ml.Movies.Remove(m);
            db.SaveChanges();
            return RedirectToAction("MovieListEdit", new { id = listId });
        }
        [HttpGet]
        public ActionResult MovieListEdit(int id)
        {
            MovieList ml = db.MovieLists.Find(id);
            var moviesInList = new List<Movie>();
            foreach (var m in db.MovieLists.Select(i => new { i.Id, i.Movies }).FirstOrDefault(i => i.Id == id).Movies)
            {
                moviesInList.Add(m);
            }
            ViewBag.moviesInList = moviesInList;
            ViewBag.movies = db.Movies.ToList();
            return View(ml);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MovieListEdit(MovieList ml)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ml).State = EntityState.Modified;
                //db.MovieLists.Find(ml.Id).movies = moviesInList;
                db.SaveChanges();
                return RedirectToAction("MovieListEdit", ml.Id);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            System.Diagnostics.Debug.WriteLine("Hello this is my error => {0}", errors);
            return RedirectToAction("MovieListEdit", ml.Id);
        }
        public ActionResult MovieListDelete(int id)
        {
            MovieList ml = db.MovieLists.Find(id);
            if (ml != null)
            {
                db.MovieLists.Remove(ml);
                db.SaveChanges();
            }

            return RedirectToAction("MovieLists");
        }

        public ActionResult SearchMovieInMovieList(string searchedMovie, int listId)
        {
            var filteredMoviesInList = new List<Movie>();
            var filteredMoviesNotInList = new List<Movie>();
            foreach (var m1 in db.Movies.ToList())
            {
                bool movieInList = false;
                if (m1.Title.ToLower().Contains(searchedMovie.ToLower()))
                {
                    foreach (var m2 in db.MovieLists.Select(i => new { i.Id, i.Movies }).FirstOrDefault(i => i.Id == listId).Movies)
                    {
                        if (m1.Id == m2.Id)
                        {
                            movieInList = true;
                            filteredMoviesInList.Add(m1);
                            break;
                        }
                    }
                    if (!movieInList)
                    {
                        filteredMoviesNotInList.Add(m1);
                    }
                }
            }
            ViewBag._filteredMoviesInList = filteredMoviesInList;
            ViewBag._filteredMoviesNotInList = filteredMoviesNotInList;
            return View(db.MovieLists.Find(listId));
        }
    }
}