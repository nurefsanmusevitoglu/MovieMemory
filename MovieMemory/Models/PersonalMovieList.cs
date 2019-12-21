using System.Collections.Generic;
using System.ComponentModel;

namespace MovieMemory.Models
{
    public class PersonalMovieList
    {
        public PersonalMovieList()
        {
            Movies = new List<Movie>();
        }
        public int Id { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        public int UserId { get; set; }

        public List<Movie> Movies { get; set; }
    }
}