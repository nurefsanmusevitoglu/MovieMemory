using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public static class Database
    {
        public static List<User> _users;
        public static List<Movie> _movies;
        public static List<Genre> _genres;
        public static List<MovieList> _movieLists;

        static Database()
        {
            _users = new List<User>() {
                new User()
                {
                    Id = 1,
                    name = "Nurefşan" ,
                    surname = "Müsevitoğlu",
                    email = "musevitoglunurefsan@gmail.com",
                    password = "1234",
                    birthday = new DateTime(1997, 7, 17),
                    gender = "female",
                    friends = new int[] { 2 },
                    movieLists = new int[] { 1, 2 }
                },
                new User()
                {
                    Id = 2,
                    name = "Hümeyra" ,
                    surname = "Esen",
                    email = "humeyra_esen@gmail.com",
                    password = "2345",
                    birthday = new DateTime(1996, 12, 23),
                    gender = "female",
                    friends = new int[] { 1 },
                    movieLists = new int[] { 1 }
                }
            };

            _movies = new List<Movie>() {
                new Movie()
                {
                    Id = 1,
                    title = "Taare Zameen Par",
                    theme = "Ishaan, a student who has dyslexia, cannot seem to get anything right at his boarding school. Soon, a new unconventional art teacher, Ram Shankar Nikumbh, helps him discover his hidden potential.",
                    director = "Aamir Khan",
                    cast = new string[] { "Darsheel Safary", "Aamir Khan", "Tanay Chheda", "Tisca Chopra", "Vipin Sharma" },
                    country = "India",
                    duration = 165,
                    releaseDate = 2007,
                    poster = "taare_zameen_par_poster.img",
                    genres = new string[] { "Drama", "Music" },
                    rating = 8.9,
                    comments = null
                },
                new Movie()
                {
                    Id = 2,
                    title = "Titanic",
                    theme = "James Cameron's Titanic is an epic, action-packed romance set against the ill-fated maiden voyage of the R.M.S. Titanic; the pride and joy of the White Star Line and, at the time, the largest moving object ever built. She was the most luxurious liner of her era -- the ship of dreams -- which ultimately carried over 1,500 people to their death in the ice cold waters of the North Atlantic in the early hours of April 15, 1912.",
                    director = "James Cameron",
                    cast = new string[] { "Leonardo DiCaprio", "Kate Winslet", "Billy Zane" },
                    country = "USA",
                    duration = 210,
                    releaseDate = 2012,
                    poster = "titanic_poster.img",
                    genres = new string[] { "Drama", "Disaster" },
                    rating = 7.8,
                    comments = null
                }
            };

            _genres = new List<Genre>() {
                new Genre() { Id = 1, genreName="Comedy" },
                new Genre() { Id = 2, genreName="Horror" },
                new Genre() { Id = 3, genreName="Adventure" },
                new Genre() { Id = 4, genreName="Romance" },
                new Genre() { Id = 5, genreName="Action" },
                new Genre() { Id = 6, genreName="Thriller" },
                new Genre() { Id = 7, genreName="Drama" },
                new Genre() { Id = 8, genreName="Mystery" },
                new Genre() { Id = 9, genreName="Crime" },
                new Genre() { Id = 10, genreName="Animation" },
                new Genre() { Id = 11, genreName="Science Ficiton" },
                new Genre() { Id = 12, genreName="Fantasy" },
                new Genre() { Id = 13, genreName="Comedy Romance" },
                new Genre() { Id = 14, genreName="Action Comedy" },
                new Genre() { Id = 15, genreName="Superhero" }
            };

            _movieLists = new List<MovieList>() {
                new MovieList() { Id = 1, movies = new int[] { 1, 2 }, title =  "Popular Movies" },
                new MovieList() { Id = 2, movies = new int[] { 1, 2 }, title =  "Coming Soon" },
                new MovieList() { Id = 3, movies = new int[] { 1, 2 }, title =  "Top Rated Movies" },
                new MovieList() { Id = 4, movies = new int[] { 1, 2 }, title =  "Best Crime & Thriller Movies" },
                new MovieList() { Id = 5, movies = new int[] { 1, 2 }, title =  "Best Movies of the 20th Century" }
            };

        }

        public static List<User> Users { get { return _users; } }
        public static List<Movie> Movies { get { return _movies; } }
        public static List<Genre> Genres { get { return _genres; } }
        public static List<MovieList> MovieLists { get { return _movieLists; } }

        public static void AddUser(User u) { _users.Add(u); }
        public static void AddMovie(Movie m) { _movies.Add(m); }
        public static void AddGenre(Genre g) { _genres.Add(g); }
        public static void AddMovieList(MovieList ml) { _movieLists.Add(ml); }

        public static User FindUser(int userID)
        {
            User user = null;
            foreach (var u in _users)
            {
                if(u.Id == userID)
                {
                    user = u;
                }
            }
            return user;
        }
        public static Movie FindMovie(int movieID)
        {
            Movie movie = null;
            foreach (var m in _movies)
            {
                if (m.Id == movieID)
                {
                    movie = m;
                }
            }
            return movie;
        }

        public static void DisplayUsers()
        {
            foreach (var user in _users)
            {
                Console.WriteLine("id: {0} --- name: {1}", user.Id, user.name);
            }
        }
    }
}