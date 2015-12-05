using System;

namespace MVC6Awakens.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
