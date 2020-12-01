using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieApp.Api.Model;
using MovieApp.Api.Repository;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMemoryCache memoryCache, IMovieRepository movieRepository)
        {
            _memoryCache = memoryCache;
            _movieRepository = movieRepository;
        }

        [HttpGet]
        [Route("repopulate")]
        public IEnumerable<Movie> Get()
        {
            var result = _memoryCache.Get<List<Movie>>("MovieList");

            #region 
            /*
             1.Way:
              if (result == null)
                return _memoryCache.Set("MovieList", _movieRepository.GetList());
              2.Way:
               if (!_memoryCache.TryGetValue("MovieList",out List<Movie> movieList))
                return _memoryCache.Set("MovieList", _movieRepository.GetList());
             */
            #endregion

            if (!_memoryCache.TryGetValue("MovieList",out List<Movie> movieList))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddHours(1);
                //options.SlidingExpiration = TimeSpan.FromDays(1);

                return _memoryCache.Set("MovieList", _movieRepository.GetList(), options);
            }
            

            return result;
        }
    }
}
