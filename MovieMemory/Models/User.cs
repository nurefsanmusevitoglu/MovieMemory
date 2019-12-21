using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class User
    {
        public User()
        {
            //Friends = new List<Friend>();
            PersonalMovieLists = new List<PersonalMovieList>();
            Ratings = new List<Rating>();
            Reviews = new List<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime JoinTime { get; set; }

        //public List<Friend> Friends { get; set; }
        public List<PersonalMovieList> PersonalMovieLists { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Review> Reviews { get; set; }

        public int GetAge()
        {
            int Age = DateTime.Now.Year - Birthday.Year;
            return Age;
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();
            foreach (var pml in PersonalMovieLists)
            {
                if (pml.Title == "Want-to-watch" || pml.Title == "Watched")
                {
                    allMovies.AddRange(pml.Movies);
                }
                else
                {
                    foreach (var movie in pml.Movies)
                    {
                        bool isMovieExist = false;
                        foreach (var m in allMovies)
                        {
                            if (m.Id == movie.Id)
                            {
                                isMovieExist = true;
                            }
                        }
                        if (!isMovieExist)
                        {
                            allMovies.Add(movie);
                        }
                    }
                }
            }
            return allMovies;
        }
    }
}