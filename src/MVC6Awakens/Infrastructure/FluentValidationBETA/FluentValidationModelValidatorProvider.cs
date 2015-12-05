using FluentValidation;

using Microsoft.AspNet.Mvc.ModelBinding.Validation;

using MVC6Awakens.Infrastructure.FluentValidationBETA.FluentValidation.Mvc6;


namespace MVC6Awakens.Infrastructure.FluentValidationBETA
{
    public class FluentValidationModelValidatorProvider : IModelValidatorProvider
    {
        public IValidatorFactory ValidatorFactory { get; private set; }


        public FluentValidationModelValidatorProvider(IValidatorFactory validatorFactory)
        {
            ValidatorFactory = validatorFactory;
        }


        public void GetValidators(ModelValidatorProviderContext context)
        {
            IValidator validator = CreateValidator(context);

            if (!IsValidatingProperty(context))
            {
                context.Validators.Add(new FluentValidationModelValidator(validator));
            }
        }

        protected virtual IValidator CreateValidator(ModelValidatorProviderContext context)
        {
            if (IsValidatingProperty(context))
            {
                return ValidatorFactory.GetValidator(context.ModelMetadata.ContainerType);
            }
            return ValidatorFactory.GetValidator(context.ModelMetadata.ModelType);
        }

        protected virtual bool IsValidatingProperty(ModelValidatorProviderContext context)
        {
            return context.ModelMetadata.ContainerType != null && !string.IsNullOrEmpty(context.ModelMetadata.PropertyName);
        }
    }
}