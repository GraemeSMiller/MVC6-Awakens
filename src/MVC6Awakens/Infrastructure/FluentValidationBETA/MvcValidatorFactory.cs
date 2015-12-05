using System;

using FluentValidation;


namespace MVC6Awakens.Infrastructure.FluentValidationBETA
{
    public class MvcValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider serviceProvider;


        public MvcValidatorFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = serviceProvider.GetService(validatorType) as IValidator;
            return validator;
        }
    }
}
