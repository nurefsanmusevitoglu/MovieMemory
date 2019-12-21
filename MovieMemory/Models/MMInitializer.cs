using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class MMInitializer : DropCreateDatabaseIfModelChanges<MMContext>
    {
        protected override void Seed(MMContext context)
        {
            List<User> _users = new List<User>()
            {
                new User()
                {
                    Name = "Nurefşan" ,
                    Surname = "Müsevitoğlu",
                    Email = "musevitoglunurefsan@gmail.com",
                    Password = "1234",
                    Birthday = new DateTime(1997, 7, 17),
                    Gender = "female",
                    Picture = "avatar.png",
                    Country = "Turkey",
                    Username = "nmusevitoglu",
                    Role = "A",
                    JoinTime = new DateTime(2019, 8, 30)
        },
                new User()
                {
                    Name = "Yusuf Nevzat" ,
                    Surname = "Şengün",
                    Email = "yusuf.nevzat.sengun@gmail.com",
                    Password = "mymmpass",
                    Birthday = new DateTime(1998, 3, 26),
                    Gender = "male",
                    Picture = "avatar.png",
                    Country = "USA",
                    Username = "ynsengun",
                    Role = "U",
                    JoinTime = new DateTime(2019, 8, 30)
                },
                new User()
                {
                    Name = "Ömer" ,
                    Surname = "Müsevitoğlu",
                    Email = "omer.musevit@gmail.com",
                    Password = "4444",
                    Birthday = new DateTime(1995, 4, 25),
                    Gender = "male",
                    Picture = "avatar.png",
                    Country = "Kenya",
                    Username = "omusevitoglu",
                    Role = "U",
                    JoinTime = new DateTime(2019, 8, 30)
                },
                new User()
                {
                    Name = "Hümeyra" ,
                    Surname = "Esen",
                    Email = "esen.humeyra@gmail.com",
                    Password = "123",
                    Birthday = new DateTime(1996, 12, 23),
                    Gender = "female",
                    Picture = "avatar.png",
                    Country = "Netherlands",
                    Username = "hesen",
                    Role = "U",
                    JoinTime = new DateTime(2019, 8, 30)
                },
                new User()
                {
                    Name = "Beste" ,
                    Surname = "Özalp",
                    Email = "beste.ozalp@gmail.com",
                    Password = "111",
                    Birthday = new DateTime(2000,1,1),
                    Gender = "female",
                    Picture = "avatar.png",
                    Country = "Turkey",
                    Username = "bozalp",
                    Role = "U",
                    JoinTime = new DateTime(2019, 8, 30)
                }
            };

            List<Movie> _movies = new List<Movie>() {
                new Movie()
                {
                    Title = "Taare Zameen Par",
                    Theme = "Ishaan, a student who has dyslexia, cannot seem to get anything right at his boarding school. Soon, a new unconventional art teacher, Ram Shankar Nikumbh, helps him discover his hidden potential.",
                    Director = "Aamir Khan",
                    Cast = new string[] { "Darsheel Safary", "Aamir Khan", "Tanay Chheda", "Tisca Chopra", "Vipin Sharma" },
                    Country = "India",
                    Duration = 165,
                    ReleaseDate = new DateTime(2007, 1, 1),
                    Poster = "taare_zameen_par_poster.jpg",
                    IMDBRating = 8.9,
                    Comments = null
                },
                new Movie()
                {
                    Title = "Titanic",
                    Theme = "James Cameron's Titanic is an epic, action-packed romance set against the ill-fated maiden voyage of the R.M.S. Titanic; the pride and joy of the White Star Line and, at the time, the largest moving object ever built. She was the most luxurious liner of her era -- the ship of dreams -- which ultimately carried over 1,500 people to their death in the ice cold waters of the North Atlantic in the early hours of April 15, 1912.",
                    Director = "James Cameron",
                    Cast = new string[] { "Leonardo DiCaprio", "Kate Winslet", "Billy Zane" },
                    Country = "USA",
                    Duration = 210,
                    ReleaseDate = new DateTime(2012, 1, 1),
                    Poster = "titanic_poster.jpg",
                    IMDBRating = 7.8,
                    Comments = null
                },
                new Movie()
                {
                    Title = "Batman Begins",
                    Theme = "Acclaimed director Christopher Nolan explores the origins of the legendary Dark Knight. After his parents' murders, disillusioned heir Bruce Wayne (Christian Bale) travels the world seeking the means to fight injustice. With the help of his trusted butler Alfred (Michael Caine), Detective Jim Gordon (Gary Oldman) and his ally Lucius Fox (morgan freeman), Wayne returns to Gotham and unleashes his alter ego: Batman, a masked crusader who uses strength, intellect and high-tech weaponry to fight evil.",
                    Director = "Christopher Nolan",
                    Cast = new string[] { "Christian Bale", "Cillian Murphy", "Katie Holmes" },
                    Country = "USA",
                    Duration = 140,
                    ReleaseDate = new DateTime(2005, 1, 1),
                    Poster = "batman_begins_poster.jpg",
                    IMDBRating = 8.2,
                    Comments = null
                },
                new Movie()
                {
                    Title = "Once Upon a Time In Hollywood",
                    Theme = "Quentin Tarantino’s Once Upon a Time... in Hollywood visits 1969 Los Angeles, where everything is changing, as TV star Rick Dalton (Leonardo DiCaprio) and his longtime stunt double Cliff Booth (Brad Pitt) make their way around an industry they hardly recognize anymore. The ninth film from the writer-director features a large ensemble cast and multiple storylines in a tribute to the final moments of Hollywood’s golden age.",
                    Director = "Christopher Nolan",
                    Cast = new string[] { "Leonardo DiCaprio", "Brad Pitt", "Margot Robbie" },
                    Country = "USA",
                    Duration = 160,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    Poster = "once_upon_a_time_in_hollywood_poster.jpg",
                    IMDBRating = 8.1,
                    Comments = null
                },
                new Movie()
                {
                    Title = "Spider-Man: Far From Home",
                    Theme = "Following the events of Avengers: Endgame, Spider-Man must step up to take on new threats in a world that has changed forever.",
                    Director = "Quentin Tarantino",
                    Cast = new string[] { "Tom Holland", "Zendaya", "Jake Gyllenheal" },
                    Country = "USA",
                    Duration = 129,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    Poster = "spiderman_poster.jpg",
                    IMDBRating = 7.9,
                    Comments = null
                },
                new Movie()
                {
                    Title = "The Lion King",
                    Theme = "Simba idolizes his father, King Mufasa, and takes to heart his own royal destiny on the plains of Africa. But not everyone in the kingdom celebrates the new cub's arrival. Scar, Mufasa's brother -- and former heir to the throne -- has plans of his own. The battle for Pride Rock is soon ravaged with betrayal, tragedy and drama, ultimately resulting in Simba's exile. Now, with help from a curious pair of newfound friends, Simba must figure out how to grow up and take back what is rightfully his.",
                    Director = "Jon Favreau",
                    Cast = new string[] { "Donald Glover", "Beyoncé", "Seth Rogen" },
                    Country = "USA",
                    Duration = 118,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    Poster = "the_lion_king_poster.jpg",
                    IMDBRating = 7.1,
                    Comments = null
                }
            };

            List<Genre> _genres = new List<Genre>()
            {
                new Genre() { GenreName="Comedy" },
                new Genre() { GenreName="Horror" },
                new Genre() { GenreName="Adventure" },
                new Genre() { GenreName="Romance" },
                new Genre() { GenreName="Action" },
                new Genre() { GenreName="Thriller" },
                new Genre() { GenreName="Drama" },
                new Genre() { GenreName="Mystery" },
                new Genre() { GenreName="Crime" },
                new Genre() { GenreName="Animation" },
                new Genre() { GenreName="Science Ficiton" },
                new Genre() { GenreName="Fantasy" },
                new Genre() { GenreName="Comedy Romance" },
                new Genre() { GenreName="Action Comedy" },
                new Genre() { GenreName="Superhero" }
            };

            List<MovieList> _movieLists = new List<MovieList>()
            {
                new MovieList() { Title =  "Popular Movies" },
                new MovieList() { Title =  "Coming Soon" },
                new MovieList() { Title =  "Top Rated Movies" },
                new MovieList() { Title =  "Best Crime & Thriller Movies" },
                new MovieList() { Title =  "Best Movies of the 20th Century" }
            };

            List<Country> _countries = new List<Country>
            {
                new Country() { CountryName = "India" },
                new Country() { CountryName = "Kenya" },
                new Country() { CountryName = "Netherlands" },
                new Country() { CountryName = "New Zeland" },
                new Country() { CountryName = "Peru" },
                new Country() { CountryName = "Russia" },
                new Country() { CountryName = "Spain" },
                new Country() { CountryName = "United Kingdom" },
                new Country() { CountryName = "USA" },
                new Country() { CountryName = "Turkey" }
            };

            foreach (var u in _users)
            {
                u.PersonalMovieLists.Add(new PersonalMovieList() { Title = "Want-to-watch" });
                u.PersonalMovieLists.Add(new PersonalMovieList() { Title = "Watched" });
                context.Users.Add(u);
            }
            foreach (var u in _movies)
            {
                context.Movies.Add(u);
            }
            foreach (var u in _genres)
            {
                context.Genres.Add(u);
            }
            foreach (var u in _movieLists)
            {
                context.MovieLists.Add(u);
            }
            foreach (var u in _countries)
            {
                context.Countries.Add(u);
            }

            context.SaveChanges();

            context.Movies.Find(1).Genres.Add(context.Genres.Find(1));
            context.Movies.Find(1).Genres.Add(context.Genres.Find(12));
            context.Movies.Find(2).Genres.Add(context.Genres.Find(4));
            context.Movies.Find(2).Genres.Add(context.Genres.Find(7));
            context.Movies.Find(3).Genres.Add(context.Genres.Find(5));
            context.Movies.Find(3).Genres.Add(context.Genres.Find(12));

            context.MovieLists.Find(1).Movies.Add(context.Movies.Find(1));
            context.MovieLists.Find(1).Movies.Add(context.Movies.Find(2));
            context.MovieLists.Find(1).Movies.Add(context.Movies.Find(3));
            context.MovieLists.Find(2).Movies.Add(context.Movies.Find(1));
            context.MovieLists.Find(2).Movies.Add(context.Movies.Find(2));
            context.MovieLists.Find(3).Movies.Add(context.Movies.Find(2));
            context.MovieLists.Find(3).Movies.Add(context.Movies.Find(3));
            context.MovieLists.Find(4).Movies.Add(context.Movies.Find(1));
            context.MovieLists.Find(4).Movies.Add(context.Movies.Find(3));
            context.MovieLists.Find(5).Movies.Add(context.Movies.Find(3));

            context.Friends.Add(new Friend() { UserId1 = 1, UserId2 = 4, IsFriend = true });
            context.Friends.Add(new Friend() { UserId1 = 1, UserId2 = 2, IsFriend = false });
            context.Friends.Add(new Friend() { UserId1 = 2, UserId2 = 3, IsFriend = true });
            context.Friends.Add(new Friend() { UserId1 = 2, UserId2 = 4, IsFriend = true });
            context.Friends.Add(new Friend() { UserId1 = 5, UserId2 = 1, IsFriend = false });

            context.PersonalMovieLists.Find(1).Movies.Add(context.Movies.Find(1));
            context.PersonalMovieLists.Find(1).Movies.Add(context.Movies.Find(2));
            context.PersonalMovieLists.Find(2).Movies.Add(context.Movies.Find(3));
            context.PersonalMovieLists.Find(3).Movies.Add(context.Movies.Find(3));
            context.PersonalMovieLists.Find(4).Movies.Add(context.Movies.Find(2));

            context.SaveChanges();


            base.Seed(context);
        }
    }
}