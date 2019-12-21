using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Genre
    {
        public Genre()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        [DisplayName("Genre Name")]
        public string GenreName { get; set; }

        public List<Movie> Movies { get; set; }
    }
}