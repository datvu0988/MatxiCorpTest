using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace matxicorp.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            return GetEnumDescriptionInternal(value);
        }

        /// <summary>
        /// Gets the description of enumvalue, if not found, returns the enumvalue.ToString()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(this T value) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException($"The supplied type {typeof(T).AssemblyQualifiedName} is not an Enum Type");

            return GetEnumDescriptionInternal(value);
        }

        private static string GetEnumDescriptionInternal(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])(fi.GetCustomAttributes(typeof(DescriptionAttribute), false));
            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Parse the enumName string to the enum type specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static T Parse<T>(string enumName) where T : struct
        {
            return (T)(Enum.Parse(typeof(T), enumName));
        }

        /// <summary>
        /// Parse the enumName string to the enum type specified
        /// </summary>
        public static T Parse<T>(string enumName, bool ignoreCase) where T : struct
        {
            try
            {
                return (T)(Enum.Parse(typeof(T), enumName, ignoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ocurred when parsing {enumName} into {typeof(T).AssemblyQualifiedName}", ex);
            }
        }

        /// <summary>
        /// Parse the enumName string to the enum type specified, and returns null if it can't be parsed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static T? ParseNullable<T>(string enumName) where T : struct
        {
            if (!string.IsNullOrEmpty(enumName) && Enum.IsDefined(typeof(T), enumName))
                return Parse<T>(enumName);
            else
                return new T?();
        }

        public static T[] GetValues<T>()
        {
            return GetValues<T, T>(x => x);
        }

        public static TResult[] GetValues<T, TResult>(Func<T, TResult> converter)
        {
            var result = new List<TResult>();
            foreach (T eachEnumValue in Enum.GetValues(typeof(T)))
            {
                result.Add(converter(eachEnumValue));
            }
            return result.ToArray();
        }

        public static bool ContainsFlag<T>(this T theValue, T theFlag)
            where T : struct
        {
            return (Convert.ToUInt64(theValue) & Convert.ToUInt64(theFlag)) != 0;
        }

        public static TResult ConvertByName<TSource, TResult>(this TSource source)
            where TSource : struct
            where TResult : struct
        {
            return Parse<TResult>(source.ToString());
        }

        public static bool TryParse<T>(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
                return true;
            }
            catch
            {
                returnedValue = default(T);
                return false;
            }
        }
    }
}
