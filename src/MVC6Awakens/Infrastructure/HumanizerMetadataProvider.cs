using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

using Humanizer;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;

namespace MVC6Awakens.Infrastructure
{
    /// <summary>
    /// Whenever a model property does not have Display Attribute, we add it here: separate words with spaces. 
    /// Also trim id off the end of any property
    /// </summary>
    public class HumanizerMetadataProvider : IDisplayMetadataProvider
    {
        private static readonly Regex IdRegex = new Regex(@"(.*)(Id[s]?)", RegexOptions.IgnoreCase & RegexOptions.Compiled);
        private static bool IsTransformRequired(
            string propertyName,
            DisplayMetadata modelMetadata,
            IReadOnlyList<object> propertyAttributes)
        {
            if (!string.IsNullOrEmpty(modelMetadata.SimpleDisplayProperty))
            {
                return false;
            }

            if (propertyAttributes.Any(a => a is DisplayAttribute))
            {
                return false;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Gets the values for properties of <see cref="T:Microsoft.AspNet.Mvc.ModelBinding.Metadata.DisplayMetadata"/>. 
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNet.Mvc.ModelBinding.Metadata.DisplayMetadataProviderContext"/>.</param>
        public void GetDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyAttributes = context.Attributes;
            var modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;
            var typeName = context.Key.ModelType.FullName;
            var name = context.Key.Name;
            if (IsTransformRequired(propertyName, modelMetadata, propertyAttributes))
            {
                if (name.Length > 2 && (typeName.Contains("Int") || (typeName.Contains("Guid"))))
                {
                    var match = IdRegex.Match(name);
                    if (match.Success)
                    {
                        name = match.Groups[1].Value;
                    }
                }
                Func<string> a = () => name.Humanize().Transform(To.TitleCase);
                modelMetadata.DisplayName = a;
            }
        }
    }
}