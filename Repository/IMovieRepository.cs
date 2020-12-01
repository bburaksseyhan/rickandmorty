using System.Collections.Generic;
using MovieApp.Api.Model;

namespace MovieApp.Api.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetList();
    }
}
