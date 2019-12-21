using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Movie
    {
        public Movie()
        {
            Genres = new List<Genre>();
            MovieLists = new List<MovieList>();
            PersonalMovieLists = new List<PersonalMovieList>();
            Ratings = new List<Rating>();
            Reviews = new List<ReviewMovie>();
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]
        public string Director { get; set; }
        public string[] Cast { get; set; }
        [Required]
        public string Country { get; set; }
        [DisplayName("Duration (min.)")]
        [Range(0, 1000, ErrorMessage = "Please enter correct value")]
        public int Duration { get; set; }
        [DisplayName("Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        [Range(0, 10, ErrorMessage = "Please enter value between 0 and 10")]
        public double IMDBRating { get; set; }
        public string[] Comments { get; set; }


        public List<Genre> Genres { get; set; }
        public List<MovieList> MovieLists { get; set; }
        public List<PersonalMovieList> PersonalMovieLists { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<ReviewMovie> Reviews { get; set; }

        public double GetRating()
        {
            double avgRating = 0, totalRating = 0;
            if (Ratings.Count != 0)
            {
                foreach (var r in Ratings)
                {
                    totalRating += r.Star;
                }
                avgRating = totalRating / Ratings.Count;
            }
            return avgRating;
        }
    }
}