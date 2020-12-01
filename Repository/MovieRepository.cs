using System;
using System.Collections.Generic;
using MovieApp.Api.Model;

namespace MovieApp.Api.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public List<Movie> GetList()
        {
            var movies = new List<Movie>();

            movies.Add(new Movie() { Id = Guid.NewGuid(), Category = "Animation, Action, Adventure ", Name = "Pokemon",
                                     Rating = "7.5/10", Year = "1997",
                                     CreatedDate = DateTime.Now , Director = "Junichi Masuda, Ken Sugimori, Satoshi Tajiri",
                                     Description = "Ash Ketchum, his yellow pet Pikachu, and his human friends explore a world of powerful creatures."
            });

            movies.Add(new Movie() { Id = Guid.NewGuid(), Category = " Animation, Adventure, Comedy", Name= "Rick and Morty",
                                     Rating = "9.2/10", Year="2003",
                                     CreatedDate = DateTime.Now, Director = "Dan Harmon, Justin Roiland",
                                     Description = "An animated series that follows the exploits of a super scientist and his not-so-bright grandson."
            });

            return movies;
        }
    }
}
