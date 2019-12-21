using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MovieMemory.Models;

namespace MovieMemory.Controllers
{
    [Authorize(Roles = "U,A")]
    public class AccountController : Controller
    {

        private MMContext db = new MMContext();
        private Friendship fs { get; set; }
        public AccountController()
        {
            fs =new Friendship();
        }

        // GET: Account
        public ActionResult Index()
        {
            db.Likes.Include(i => i.User).ToList();
            db.Reviews.Include(i => i.User).ToList();

            List<User> userFriends = GetFriends();
            var friendUpdates = new List<FriendUpdate>();
            var movieListUpdates = new List<MovieListUpdate>();
            var movieRatingUpdates = new List<MovieRatingUpdate>();
            foreach (var friend in userFriends)
            {
                var updates = db.Updates.Where(i => i.User.Id == friend.Id)
                    .Include(i => i.User)
                    .Include(i => i.Likes)
                    .Include(i => i.Reviews);
                friendUpdates.AddRange(updates.OfType<FriendUpdate>()
                    .Include(i => i.Friend.User1).ToList());
                movieListUpdates.AddRange(updates.OfType<MovieListUpdate>()
                    .Include(i => i.Movie).ToList());
                movieRatingUpdates.AddRange(updates.OfType<MovieRatingUpdate>()
                    .Include(i => i.Movie).ToList());
            }
            friendUpdates = friendUpdates.OrderByDescending(i => i.UpdateTime).ToList();
            movieListUpdates = movieListUpdates.OrderByDescending(i => i.UpdateTime).ToList();
            movieRatingUpdates = movieRatingUpdates.OrderByDescending(i => i.UpdateTime).ToList();

            //var movieRatingUpdates = (from i in db.Updates.OfType<MovieRatingUpdate>() select i).ToList();

            var updateModel = Tuple.Create(friendUpdates, movieListUpdates, movieRatingUpdates);

            return View(updateModel);
        }

        public new ActionResult Profile(string username)
        {
            User user = db.Users.Include(i => i.PersonalMovieLists).FirstOrDefault(i => i.Username == username);
            /// This function is to fill the movies in the personal movie lists
            foreach (var pml in user.PersonalMovieLists)
            {
                pml.Movies = db.PersonalMovieLists.Include(i => i.Movies).FirstOrDefault(i => i.Id == pml.Id).Movies;
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Settings()
        {
            User u = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            ViewBag._countries = db.Countries.Select(i => i.CountryName).ToList();
            ViewBag._users = db.Users.Select(i => i.Username).ToList();
            return View(u);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(User u)
        {
            User temp = db.Users.AsNoTracking().FirstOrDefault(x => x.Username == u.Username);
            if (temp != null && temp.Id != u.Id)
            {
                ViewBag.usernameErrorMessage = "*This username is already in use!";
                ViewBag._countries = db.Countries.Select(i => i.CountryName).ToList();
                return View(u);
            }
            else if (ModelState.IsValid)
            {
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                if (temp == null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                }
                return RedirectToAction("Profile");
            }
            ViewBag._countries = db.Countries.Select(i => i.CountryName).ToList();
            return View(u);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string Password, string Newpassword, string Repassword)
        {
            User u = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            if (Password != u.Password)
            {
                @ViewBag.oldPasswordErrorMessage = "*Password is wrong!";
                return View();
            }
            else if (Newpassword != Repassword)
            {
                @ViewBag.newPasswordErrorMessage = "*Passwords do not match!";
                return View();
            }
            else
            {
                u.Password = Newpassword;
                db.SaveChanges();
                return RedirectToAction("Settings");
            }
        }

        /// <summary>
        /// Functions to handle users' friend operation
        /// </summary>
        public ActionResult Friends()
        {
            fs.Friends = GetFriends();
            fs.WaitingFriends = GetWaitingFriends();
            fs.RequestedFriends = GetRequestedFriends();
            return View(fs);
        }

        [HttpPost]
        public ActionResult SearchUser(string searchedUser)
        {
            fs.Friends = GetFriends();
            fs.WaitingFriends = GetWaitingFriends();
            fs.RequestedFriends = GetRequestedFriends();
            ViewBag.friendship = fs;
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
            return View(filteredUsers);
        }

        public ActionResult AddFriend(int id)
        {
            var user = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            Friend f = db.Friends.FirstOrDefault(i => (i.UserId1 == user.Id && i.UserId2 == id) || (i.UserId1 == id && i.UserId2 == user.Id));
            if (f == null)
            {
                f = new Friend() { UserId1 = user.Id, UserId2 = id, IsFriend = false };
                db.Friends.Add(f);
            }
            db.SaveChanges();

            return RedirectToAction("Friends");
        }

        public ActionResult AnswerFriend(int id, bool isAccepted)
        {
            User user = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            var friend = db.Friends.Include(i => i.User1).FirstOrDefault(i => i.UserId1 == id && i.UserId2 == user.Id);
            if (isAccepted)
            {
                friend.IsFriend = true;
                FriendUpdate fu = new FriendUpdate()
                {
                    UpdateTime = DateTime.Now,
                    User = user,
                    Friend = friend
                };
                db.Updates.Add(fu);
            }
            else
            {
                db.Friends.Remove(friend);
            }
            db.SaveChanges();

            return RedirectToAction("Friends");
        }

        public ActionResult DeleteFriend(int id)
        {
            int currentUserId = (db.Users.Where(i => i.Username == User.Identity.Name).Select(i => i.Id).ToList())[0];
            var v = db.Friends.Where(i => i.UserId1 == id && i.UserId2 == currentUserId || i.UserId1 == currentUserId && i.UserId2 == id).ToList();
            var friend = db.Friends.Where(i => i.UserId1 == id && i.UserId2 == currentUserId || i.UserId1 == currentUserId && i.UserId2 == id).ToList()[0];
            db.Friends.Remove(friend);
            db.SaveChanges();

            return RedirectToAction("Friends");
        }

        public PartialViewResult Recommendation(string username)
        {
            //var u = db.Users.FirstOrDefault(x => x.userName == username);
            //List<Movie> userMovieList = new List<Movie>();
            //foreach (var userMovieId in db.Users.Find(u.Id).movieLists)
            //{
            //    userMovieList.Add(db.Movies.Find(userMovieId));
            //}
            return PartialView(db.Movies.ToList());
        }

        ///------------------Functions returns friend lists----------------------
        public List<User> GetFriends()
        {
            int currentUserId = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name).Id;
            List<User> friends = db.Friends.Where(i => i.UserId1 == currentUserId && i.IsFriend).Select(i => i.User2).ToList();
            List<User> friends2 = db.Friends.Where(i => i.UserId2 == currentUserId && i.IsFriend).Select(i => i.User1).ToList();
            friends.AddRange(friends2);

            return friends;
        }
        /// Returns friends who want to be friend with current user
        public List<User> GetWaitingFriends()
        {
            int currentUserId = (db.Users.Where(i => i.Username == User.Identity.Name).Select(i => i.Id).ToList())[0];
            var waitingFriends = db.Friends.Where(i => i.UserId2 == currentUserId && !i.IsFriend).Select(i => i.User1).ToList();

            return waitingFriends;
        }
        /// Returns users that current user have sent friendship request
        public List<User> GetRequestedFriends()
        {
            int currentUserId = (db.Users.Where(i => i.Username == User.Identity.Name).Select(i => i.Id).ToList())[0];
            var requestedFriends = db.Friends.Where(i => i.UserId1 == currentUserId && !i.IsFriend).Select(i => i.User2).ToList();

            return requestedFriends;
        }

        /// <summary>
        /// Functions to handle users' personal movie operation
        /// </summary>
        public ActionResult Movies()
        {
            User user = db.Users.Include(i => i.PersonalMovieLists).FirstOrDefault(i => i.Username == User.Identity.Name);
            db.PersonalMovieLists.Where(i => i.UserId == user.Id).Include(i => i.Movies).ToList();
            ViewBag.allMovies = user.GetAllMovies();

            return View(user.PersonalMovieLists);
        }
        public PartialViewResult ShowMovieList(int id)
        {
            var movies = db.PersonalMovieLists.Include(i => i.Movies).FirstOrDefault(i => i.Id == id).Movies;
            return PartialView(movies);
        }
        [HttpGet]
        public ActionResult AddMovieList()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovieList([Bind(Include = "Id, Title")] PersonalMovieList pml)
        {
            if (ModelState.IsValid)
            {
                //pml.movies = new List<Movie>();
                pml.UserId = (db.Users.Where(i => i.Username == User.Identity.Name).Select(i => i.Id).ToList())[0];
                db.PersonalMovieLists.Add(pml);
                db.SaveChanges();
                return RedirectToAction("Movies");
            }
            return View(pml);
        }
        public ActionResult DeleteMovieList(int id)
        {
            var temp = db.PersonalMovieLists.Find(id);
            if (temp != null)
            {
                db.PersonalMovieLists.Remove(temp);
                db.SaveChanges();
                return RedirectToAction("Movies");
            }
            return RedirectToAction("Movies");
        }
        public PartialViewResult AddMovieToPersonalMovieListDropdown(int movieId)
        {
            ViewBag.movieId = movieId;
            int currentUserId = (db.Users.Where(i => i.Username == User.Identity.Name).Select(i => i.Id).ToList())[0];
            List<PersonalMovieList> PMLsOfMovie = db.Movies.Include(i => i.PersonalMovieLists).FirstOrDefault(i => i.Id == movieId).PersonalMovieLists.Where(i => i.UserId == currentUserId).ToList();
            ViewBag.PMLsOfMovie = PMLsOfMovie;
            var PMLsOfUser = db.PersonalMovieLists.Where(i => i.UserId == currentUserId).Include(i => i.Movies).ToList();

            return PartialView(PMLsOfUser);
        }
        public ActionResult AddMovieToPersonalMovieList(int listId, int movieId)
        {
            PersonalMovieList PML = db.PersonalMovieLists.Include(a => a.Movies).FirstOrDefault(i => i.Id == listId);
            if (PML.Title == "Watched" || PML.Title == "Want-to-watch")
            {
                Movie M = db.Movies.Include(a => a.PersonalMovieLists).FirstOrDefault(i => i.Id == movieId);
                foreach (var pml in M.PersonalMovieLists)
                {
                    if (pml.Title == "Watched" || pml.Title == "Want-to-watch")
                    {
                        M.PersonalMovieLists.Remove(pml);
                        db.SaveChanges();
                        break;
                    }
                }
                MovieListUpdate mlu = db.Updates.OfType<MovieListUpdate>().FirstOrDefault(i => i.User.Id == PML.UserId && i.Movie.Id == movieId);
                if (mlu != null)
                {
                    mlu.UpdateTime = DateTime.Now;
                    mlu.Content = PML.Title;
                }
                else
                {
                    mlu = new MovieListUpdate()
                    {
                        UpdateTime = DateTime.Now,
                        User = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name),
                        Movie = db.Movies.Find(movieId),
                        Content = PML.Title
                    };
                    db.Updates.Add(mlu);
                }
            }
            db.PersonalMovieLists.Find(listId).Movies.Add(db.Movies.Find(movieId));

            db.SaveChanges();
            return RedirectToAction("Movies");
        }
        public ActionResult RemoveMovieFromPersonalMovieList(int listId, int movieId)
        {
            PersonalMovieList PML = db.PersonalMovieLists.Include(a => a.Movies).FirstOrDefault(i => i.Id == listId);
            foreach (var movie in PML.Movies)
            {
                if (movie.Id == movieId)
                {
                    PML.Movies.Remove(movie);
                    db.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Movies");
        }

        public PartialViewResult StarRatingBar(int movieId)
        {
            int stars = 0;
            Rating r = db.Ratings.FirstOrDefault(i => i.UserId == db.Users.FirstOrDefault(a => a.Username == User.Identity.Name).Id && i.MovieId == movieId);
            if (r != null)
            {
                stars = r.Star;
            }
            return PartialView(Tuple.Create(movieId, stars));
        }
        [HttpPost]
        public ActionResult StarRating(int movieId, int rating)
        {
            User u = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            Rating r = db.Ratings.FirstOrDefault(i => i.MovieId == movieId && i.UserId == u.Id);
            if (r == null)
            {
                r = new Rating();
                r.MovieId = movieId;
                r.UserId = u.Id;
                r.Star = rating;
                db.Ratings.Add(r);
            }
            else
            {
                r.Star = rating;
            }

            MovieRatingUpdate mru = db.Updates.OfType<MovieRatingUpdate>().FirstOrDefault(i => i.User.Id == u.Id && i.Movie.Id == movieId);
            if (mru != null)
            {
                mru.UpdateTime = DateTime.Now;
                mru.Rating = rating;
            }
            else
            {
                mru = new MovieRatingUpdate()
                {
                    UpdateTime = DateTime.Now,
                    User = u,
                    Movie = db.Movies.Find(movieId),
                    Rating = rating
                };
                db.Updates.Add(mru);
            }
            
            db.SaveChanges();
            return RedirectToAction("MovieInfo", "Home", new { id = movieId });
        }
        [HttpPost]
        public ActionResult CommentMovie(int movieId, string comment)
        {
            User u = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            ReviewMovie r = new ReviewMovie();
            r.MovieId = movieId;
            r.User = u;
            r.Comment = comment;
            r.ReviewTime = DateTime.Now;
            db.Reviews.Add(r);
            db.SaveChanges();
            return RedirectToAction("MovieInfo", "Home", new { id = movieId });
        }

        [HttpPost]
        public ActionResult CommentUpdate(int updateId, string comment)
        {
            User u = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            ReviewUpdate r = new ReviewUpdate();
            r.UpdateId = updateId;
            r.User = u;
            r.Comment = comment;
            r.ReviewTime = DateTime.Now;
            db.Reviews.Add(r);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LikeUpdate(int updateId)
        {
            User user = db.Users.FirstOrDefault(i => i.Username == User.Identity.Name);
            Like like = new Like() {
                UserId = user.Id,
                UpdateId = updateId
            };
            db.Updates.Find(updateId).Likes.Add(like);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DoNotLikeUpdate(int updateId)
        {
            Update update = db.Updates.Include(i => i.Likes).FirstOrDefault(i => i.Id == updateId);
            Like like = update.Likes.FirstOrDefault(i => i.UpdateId == updateId);
            db.Likes.Remove(like);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}