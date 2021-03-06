﻿using System;
using System.ComponentModel.DataAnnotations;

using MVC6Awakens.Infrastructure.ModelBinders;


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

        public Guid? SpeciesId { get; set; }
    }
}
