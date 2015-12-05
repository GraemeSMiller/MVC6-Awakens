using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;

using Microsoft.AspNet.Mvc.ModelBinding.Validation;


namespace MVC6Awakens.Infrastructure.FluentValidationBETA
{
    namespace FluentValidation.Mvc6
    {
        //TODO: Need support for CustomizeValidatorAttribute and client-side

        public class FluentValidationModelValidator : IModelValidator
        {
            private readonly IValidator validator;

            public FluentValidationModelValidator(IValidator validator)
            {
                this.validator = validator;
            }

            public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
            {
                var model = context.Model;
                var result = validator.Validate(model);
                var errors = result.Errors;
                return errors.Select(error => new ModelValidationResult(error.PropertyName, error.ErrorMessage));
            }

            public bool IsRequired
            {
                get { return false; }
            }
        }
    }
}
