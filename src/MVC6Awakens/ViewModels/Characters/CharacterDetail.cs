using System;
using System.ComponentModel.DataAnnotations;

namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterDetail
    {
        [Display(Name = "Test")]
        public string WeaponOfChoice { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HomePlanetName { get; set; }

        public string SpeciesName { get; set; }
    }
}
