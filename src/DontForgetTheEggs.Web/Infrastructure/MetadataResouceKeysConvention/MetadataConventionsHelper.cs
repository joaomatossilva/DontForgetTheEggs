using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DontForgetTheEggs.Web.Infrastructure.MetadataResouceKeysConvention.Extensions;

namespace DontForgetTheEggs.Web.Infrastructure.MetadataResouceKeysConvention
{
    public static class MetadataConventionsHelper
    {
        public static string GetEnumValue(Enum value, Type resourceType)
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString())
                .First();
            return GetDisplayName(type, memberInfo, resourceType);
        }

        /// <summary>
        /// Helper method used by the FluentValidator to get the Display name value of a type's property
        /// </summary>
        /// <param name="containerType"></param>
        /// <param name="memberInfo"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static string GetDisplayName(Type containerType, MemberInfo memberInfo, Type resourceType)
        {
            //if the type already has a DisplayAttribute use it, instead
            var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>() ?? new DisplayAttribute();
            //use the DisplayAttribute resource type if attribute exists and the type is specified
            displayAttribute.ResourceType = displayAttribute.ResourceType ?? resourceType;

            var displayAttributeName = displayAttribute.Name;
            if (String.IsNullOrEmpty(displayAttributeName))
            {
                displayAttributeName = GetDisplayAttributeName(containerType, memberInfo.Name, resourceType);
            }
            //if the displayName exists on the resource type, use it
            if (displayAttribute.ResourceType != null && displayAttribute.ResourceType.PropertyExists(displayAttributeName))
            {
                return (string)displayAttribute.ResourceType.GetProperty(displayAttributeName)?.GetValue(null);
            }
            return displayAttributeName;
        }

        /// <summary>
        /// Locates (if exists) the display name key on the resource type
        /// </summary>
        /// <param name="containerType"></param>
        /// <param name="propertyName"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static string GetDisplayAttributeName(Type containerType, string propertyName, Type resourceType)
        {
            if (containerType == null)
            {
                return null;
            }

            //If type is enum, search resource on "Enum_Type_Value" key
            if (containerType.IsEnum)
            {
                var key = $"Enum_{containerType.Name}_{propertyName}";
                if (resourceType.PropertyExists(key))
                {
                    return key;
                }
                //if it doesn't don't follow the normal flow, just output the value
                return propertyName;
            }

            // check to see that resource key with the convention name exists.
            string resourceKey = GetResourceKey(containerType, propertyName);
            if (resourceType.PropertyExists(resourceKey))
            {
                return resourceKey;
            }
    
            //try if we find the generic key "Property_propertyName"
            var defaultPropertyKey = "Property_" + propertyName;
            if (resourceType.PropertyExists(defaultPropertyKey))
            {
                return defaultPropertyKey;
            }

            return propertyName;
        }

        //This is the core of the Convention.
        //The resource key is composed of the FeatureName_ActionCommand_Property
        public static string GetResourceKey(Type containerType, string propertyName)
        {
            var containerTypeName = containerType.FullName;
            if (string.IsNullOrEmpty(containerTypeName))
            {
                return propertyName;
            }
            //container name should have something like Micolandia.Core.Features.Index.Home+Command
            //we want to strip down all from "Features." below (inclusive)
            const string markerString = "Features.";
            var indexOfFeatures = containerTypeName.IndexOf(markerString, StringComparison.Ordinal);
            if (indexOfFeatures > 0)
            {
                containerTypeName = containerTypeName.Substring(indexOfFeatures + markerString.Length);
            }
            // Now our container name should have something like Index.Home+Command
            // and we now want to replace the '.' and '+' and add the propertyname in question
            //so that we endup with something like this Index_HomeCommand_Question
            return containerTypeName.Replace(".", "_").Replace("+", "") + "_" + propertyName;
        }
    }
}