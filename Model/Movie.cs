using System;
namespace MovieApp.Api.Model
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public string Description { get; set; }

        public string Rating { get; set; }

        public string Director { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
