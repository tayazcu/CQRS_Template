using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Reflection;
using Project.Framework.Domain;
using System.ComponentModel;

namespace Project.Framework.Extensions
{
    public static class EnumExtension
    {
        public static int ToInt<TValue>(this TValue value) where TValue : struct, IConvertible
        {
            if (!typeof(TValue).IsEnum)
            {
                throw new ArgumentException(nameof(value));
            }

            return Convert.ToInt32(value);
        }
        public static string ToString<TValue>(this TValue value) where TValue : struct, IConvertible
        {
            if (!typeof(TValue).IsEnum)
            {
                throw new ArgumentException(nameof(value));
            }

            return Enum.GetName(typeof(TValue), value);
        }

        public static string ToDescription<T>(this string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
        public static string ToDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            //Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static T ToEnum<T>(this int param)
        {
            var info = typeof(T);
            if (info.IsEnum)
            {
                T result = (T)Enum.Parse(typeof(T), param.ToString(), true);
                return result;
            }

            return default(T);
        }
        public static T ToEnum<T>(this string str) where T : struct
        {
            try
            {
                T res = (T)Enum.Parse(typeof(T), str);
                if (!Enum.IsDefined(typeof(T), res)) return default(T);
                return res;
            }
            catch
            {
                return default(T);
            }
        }


        public static IEnumerable<KeyValueTemplate> ConvertToList<T>()
        {
            IEnumerable<KeyValueTemplate> list = Enum.GetValues(typeof(T)).Cast<T>()
                .Select(s => new KeyValueTemplate
                {
                    Key = s.ToString(),
                    Value = Convert.ToInt32(s)
                }).ToList();
            return list;
        }
        public static IEnumerable<string> ConvertToStringList<T>()
        {
            IEnumerable<string> list = Enum.GetValues(typeof(T)).Cast<T>().Select(s => s.ToString()).ToList();
            return list;
        }






        //public static Dictionary<int, string> ToDictionary()
        //{
        //    Type enumType = value.GetType();

        //    Dictionary<int, string> mydic = new Dictionary<int, string>();
        //    foreach (var item in Enum.GetValues(typeof(T)))
        //    {
        //        int key = (int)item;
        //        string value = item.ToString();
        //        string valueDisplay = GetDisplayAttribute();
        //        mydic.Add((int)item, item.ToString());
        //    }
        //    return mydic;
        //}

        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }

        public static Dictionary<int, string> ToDictionary(Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => q.ToDisplay());
        }

        public static Dictionary<int, string> ToDictionary<T>()
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new Exception("Type parameter should be of enum type");

            var result = Enum.GetValues(enumType).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => q.ToDisplay());

            return result;
        }
    }
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}

//// int to enum
//int enumValue = 1;
//Claims intToEnum = enumValue.ToEnum<Claims>();

//// string to enum
//string enumKey = "News";
//Claims stringToEnum = enumKey.ToEnum<Claims>();

//// enum to string
//Claims claim4 = Claims.News;
//string enumToString = claim4.ToString<Claims>();

//// enum to int
//Claims claim3 = Claims.News;
//int enumToInt = claim3.ToInt<Claims>();

//// get description from Enum
//var enumToDescription1 = claim3.ToDescription<Claims>();

//// get description from string
//var stringToDescription2 = enumKey.ToDescription<Claims>();

//// get description from Enum
//var enumToDisplay = claim3.ToDisplay();