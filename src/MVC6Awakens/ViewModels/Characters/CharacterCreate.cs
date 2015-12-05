using System;

namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterCreate
    {
        public CharacterCreate()
        {
            Id = Guid.NewGuid();
        }
        public string WeaponOfChoice { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? HomePlanetId { get; set; }
    }
}
