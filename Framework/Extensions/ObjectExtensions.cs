using System;

namespace Framework.Extensions
{
    public static class ObjectExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this object instance) where TAttribute : Attribute
        {
            var type = instance.GetType();
            var attributes = type.GetCustomAttributes(typeof(TAttribute), true);

            if (attributes.Length == 0)
                return default(TAttribute);

            var attribute = (TAttribute)attributes[0];
            return attribute;
        }
    }
}