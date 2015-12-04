using System;

namespace MVC6Awakens.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WeaponOfChoice { get; set; }
        public virtual Planet HomePlanet { get; set; }
        public Guid HomePlanetId { get; set; }
        public bool Visible { get; set; }
    }
}
