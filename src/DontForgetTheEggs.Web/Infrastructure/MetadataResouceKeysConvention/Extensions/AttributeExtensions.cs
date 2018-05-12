using System;
using System.Linq;
using System.Reflection;

namespace DontForgetTheEggs.Web.Infrastructure.MetadataResouceKeysConvention.Extensions
{
    public static class AttributeExtensions
    {
        public static TAttribute First<TAttribute>(this ICustomAttributeProvider attributeProvider)
            where TAttribute : Attribute
        {
            return attributeProvider.GetCustomAttributes(typeof (TAttribute), true).FirstOrDefault() as TAttribute;
        }
    }
}