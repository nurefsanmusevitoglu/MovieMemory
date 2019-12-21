using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewTime { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

    }

    public class ReviewMovie : Review
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }

    public class ReviewUpdate : Review
    {
        public int UpdateId { get; set; }
        public Update Update { get; set; }
    }
}