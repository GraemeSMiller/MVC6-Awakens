using System;

using FluentValidation;


namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterCreateValidator : AbstractValidator<CharacterCreate>
    {
        public CharacterCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Character name is required.").Must(NotLuke).WithMessage("Come up with something more original");
        }


        public bool NotLuke(string name)
        {
            if (name == "Luke")
            {
                return false;
            }
            return true;
        }
    }
}
