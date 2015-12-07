using System;
using System.ComponentModel.DataAnnotations;


namespace MVC6Awakens.Models
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
