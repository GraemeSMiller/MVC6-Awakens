using System;
using System.ComponentModel.DataAnnotations;


namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterCreate
    {
        public CharacterCreate()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid? PlanetId { get; set; }
    }
}
