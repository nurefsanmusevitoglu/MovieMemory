using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieMemory.Models;

namespace MovieMemory.Controllers
{
    [AllowAnonymous]
    public class MovieController : Controller
    {
        MMContext db = new MMContext();

        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Genres()
        {
            return PartialView(db.Genres.ToList());
        }

        public PartialViewResult MovieLists()
        {
            return PartialView(db.MovieLists.ToList());
        }

        public ActionResult GenreList(int id)
        {
            ViewBag.genre = db.Genres.Find(id);
            var genres = db.Genres.Where(x=>x.Id==id).Select(a => new { a.Movies }).ToList();
            var movies = new List<Movie>();
            foreach (var g in genres)
            {
                foreach (var m in g.Movies)
                {
                    movies.Add(m);
                }
            }
            return View(movies);
        }

        public ActionResult MovieList(int id)
        {
            ViewBag.movieList = db.MovieLists.Find(id);
            var movieLists = db.MovieLists.Where(x => x.Id == id).Select(a => new { a.Movies }).ToList();
            var movies = new List<Movie>();
            foreach (var ml in movieLists)
            {
                foreach (var m in ml.Movies)
                {
                    movies.Add(m);
                }
            }
            return View(movies);
        }

        [HttpPost]
        public ActionResult SearchMovieInList(string searchedMovie, int listId)
        {
            ViewBag.movieList = db.MovieLists.Find(listId);
            var movieLists = db.MovieLists.Where(x => x.Id == listId).Select(a => new { a.Movies }).ToList()[0];
            var filteredMovies = new List<Movie>();
            foreach (var m in movieLists.Movies)
            {
                if (m.Title.ToLower().Contains(searchedMovie.ToLower()))
                {
                    filteredMovies.Add(m);
                }
            }
            return View("MovieList", filteredMovies);
        }

        [HttpPost]
        public ActionResult SearchMovieInGenre(int genreId, string searchedMovie)
        {
            ViewBag.genre = db.Genres.Find(genreId);
            var movieLists = db.Genres.Where(x => x.Id == genreId).Select(a => new { a.Movies }).ToList()[0];
            var filteredMovies = new List<Movie>();
            foreach (var m in movieLists.Movies)
            {
                if (m.Title.ToLower().Contains(searchedMovie.ToLower()))
                {
                    filteredMovies.Add(m);
                }
            }
            return View("GenreList", filteredMovies);
        }
    }
}