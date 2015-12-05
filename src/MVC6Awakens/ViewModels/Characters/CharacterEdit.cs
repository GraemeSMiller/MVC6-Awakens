using System;
using System.ComponentModel.DataAnnotations;


namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterEdit
    {
        public string WeaponOfChoice { get; set; }
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid? HomePlanetId { get; set; }

        public string CreatorId { get; set; }

        public Guid? SpeciesId { get; set; }
    }
}
