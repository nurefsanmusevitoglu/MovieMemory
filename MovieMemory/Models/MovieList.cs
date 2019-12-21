using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class MovieList
    {
        public MovieList()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }

        public List<Movie> Movies { get; set; }
    }
}