using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public abstract class Update
    {
        public Update()
        {
            Likes = new List<Like>();
            Reviews = new List<ReviewUpdate>();
        }

        public int Id { get; set; }
        public DateTime UpdateTime { get; set; }
        //public int UserId { get; set; }

        public User User { get; set; }
        public List<Like> Likes { get; set; }
        public List<ReviewUpdate> Reviews { get; set; }
    }

    public class FriendUpdate : Update
    {
        public Friend Friend { get; set; }
    }

    public abstract class MovieUpdate : Update 
    {
        public Movie Movie { get; set; }
    }

    public class MovieListUpdate : MovieUpdate
    {
        public string Content { get; set; }
    }

    public class MovieRatingUpdate : MovieUpdate
    {
        public int Rating { get; set; }
    }

}