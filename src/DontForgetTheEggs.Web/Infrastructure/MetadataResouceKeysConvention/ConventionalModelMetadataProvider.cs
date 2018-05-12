using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DontForgetTheEggs.Web.Infrastructure.MetadataResouceKeysConvention.Extensions;

namespace DontForgetTheEggs.Web.Infrastructure.MetadataResouceKeysConvention
{
	/// <summary>
	/// This is a convention based Display name locator for metadata.
	/// The goals of this class is to override the MVC engine to allow fecthing the display names values
	/// on the resources based on the property names of the Models
	/// 
	/// based on https://haacked.com/archive/2011/07/14/model-metadata-and-validation-localization-using-conventions.aspx/
	/// </summary>
	public class ConventionalModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        public ConventionalModelMetadataProvider()
            : this(null)
        {
        }

        public ConventionalModelMetadataProvider(Type defaultResourceType)
        {
            DefaultResourceType = defaultResourceType;
        }

        // Whether or not the conventions only apply to classes with the MetadataConventionsAttribute attribute applied.
        public Type DefaultResourceType { get; private set; }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
            Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var attributesList = attributes.ToArray();

            Func<IEnumerable<Attribute>, ModelMetadata> metadataFactory =
                attr => base.CreateMetadata(attr, containerType, modelAccessor, modelType, propertyName);

            var conventionType = containerType ?? modelType;

            var defaultResourceType = DefaultResourceType;
            
            ApplyConventionsToValidationAttributes(attributesList, conventionType, propertyName, defaultResourceType);

            var foundDisplayAttribute = attributesList.FirstOrDefault(a => a is DisplayAttribute) as DisplayAttribute;

            if (foundDisplayAttribute.CanSupplyDisplayName())
            {
                return metadataFactory(attributesList);
            }

            // Our displayAttribute is lacking. Time to get busy.
            var displayAttribute = foundDisplayAttribute.Copy() ?? new DisplayAttribute();

            var rewrittenAttributes = attributesList.Replace(foundDisplayAttribute, displayAttribute);

            // ensure resource type.
            displayAttribute.ResourceType = displayAttribute.ResourceType ?? defaultResourceType;

            if (displayAttribute.ResourceType != null)
            {
				// ensure resource name
	            if (String.IsNullOrEmpty(displayAttribute.Name))
	            {
		            string displayAttributeName = MetadataConventionsHelper.GetDisplayAttributeName(containerType, propertyName, displayAttribute.ResourceType);
		            if (displayAttributeName != null)
		            {
			            displayAttribute.Name = displayAttributeName;
		            }
	            }
	            if (!displayAttribute.ResourceType.PropertyExists(displayAttribute.Name))
                {
                    displayAttribute.ResourceType = null;
                }
            }

            ModelMetadata metadata = metadataFactory(rewrittenAttributes);
            if (metadata.DisplayName == null || metadata.DisplayName == metadata.PropertyName)
            {
                metadata.DisplayName = metadata.PropertyName.SplitUpperCaseToString();
            }
            return metadata;
        }

        private static void ApplyConventionsToValidationAttributes(IEnumerable<Attribute> attributes, Type containerType,
            string propertyName, Type defaultResourceType)
        {
            foreach (var validationAttribute in attributes.OfType<ValidationAttribute>())
            {
                var defaultErrorMessage = GetDefaultErrorMessage(validationAttribute);
                if (string.IsNullOrEmpty(validationAttribute.ErrorMessage) || validationAttribute.ErrorMessage == defaultErrorMessage)
                {
                    string attributeShortName = validationAttribute.GetType().Name.Replace("Attribute", "");
                    string resourceKey = MetadataConventionsHelper.GetResourceKey(containerType, propertyName) + "_" + attributeShortName;

                    var resourceType = validationAttribute.ErrorMessageResourceType ?? defaultResourceType;

                    if (!resourceType.PropertyExists(resourceKey))
                    {
                        resourceKey = propertyName + "_" + attributeShortName;
                        if (!resourceType.PropertyExists(resourceKey))
                        {
                            resourceKey = "Error_" + attributeShortName;
                            if (!resourceType.PropertyExists(resourceKey))
                            {
                                continue;
                            }
                        }
                    }

                    validationAttribute.ErrorMessageResourceType = resourceType;
                    validationAttribute.ErrorMessageResourceName = resourceKey;
                }
            }
        }

        private static string GetDefaultErrorMessage(ValidationAttribute validationAttribute)
        {
            string defaultErrorMessage = null;
            if (validationAttribute is DataTypeAttribute)
            {
                try
                {
                    defaultErrorMessage = ((ValidationAttribute)Activator.CreateInstance(validationAttribute.GetType())).ErrorMessage;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return defaultErrorMessage;
        }

        
    }
}