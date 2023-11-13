using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChessExample.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType().GetMember(value.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName() ?? string.Empty;
        }

        public static T GetEnumFromDisplayName<T>(this string display) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(DisplayAttribute)) is DisplayAttribute attribute)
                {
                    if (attribute.GetName() == display)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(display));
            // Or return default(T);
        }

        public static T GetEnumFromDescription<T>(this string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(ChessBoardColumnAttribute)) is ChessBoardColumnAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public static int GetDescriptionFromValue<T>(this string value) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(ChessBoardColumnAttribute)) is ChessBoardColumnAttribute attribute)
                {
                    if (attribute.Value == value.ToUpper())
                    {
                        ChessBoardColumnAttribute[] attributes = (ChessBoardColumnAttribute[])field.GetCustomAttributes(typeof(ChessBoardColumnAttribute), false);
                        if (attributes.Length > 0)
                        {
                            return Convert.ToInt32(attributes[0].Description);
                        }
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
            // Or return default(T);
        }

        public static int GetDescriptionFromValue<T>(this char value) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(ChessBoardColumnAttribute)) is ChessBoardColumnAttribute attribute)
                {
                    if (attribute.Value == value.ToString().ToUpper())
                    {
                        ChessBoardColumnAttribute[] attributes = (ChessBoardColumnAttribute[])field.GetCustomAttributes(typeof(ChessBoardColumnAttribute), false);
                        if (attributes.Length > 0)
                        {
                            return Convert.ToInt32(attributes[0].Description);
                        }
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
            // Or return default(T);
        }

        public static T GetEnumFromDescription<T>(this int description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(ChessBoardColumnAttribute)) is ChessBoardColumnAttribute attribute)
                {
                    if (Convert.ToInt32(attribute.Description) == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else if (Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) is DescriptionAttribute attribute1)
                {
                    if (Convert.ToInt32(attribute1.Description) == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public static T GetEnumFromValue<T>(this string value) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(ChessBoardColumnAttribute)) is ChessBoardColumnAttribute attribute)
                {
                    if (attribute.Value == value)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException("Not found.", nameof(value));
            // Or return default(T);
        }

        public static int GetDescriptionFromEnum(this Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            ChessBoardColumnAttribute[] attributes = (ChessBoardColumnAttribute[])fi.GetCustomAttributes(typeof(ChessBoardColumnAttribute), false);
            DescriptionAttribute[] attributes1 = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return Convert.ToInt32(attributes[0].Description);
            }
            else if (attributes1.Length > 0)
            {
                return Convert.ToInt32(attributes1[0].Description);
            }
            else
            {
                return Convert.ToInt32(value.ToString());
            }
        }

        public static string GetValueFromEnum(this Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            ChessBoardColumnAttribute[] attributes = (ChessBoardColumnAttribute[])fi.GetCustomAttributes(typeof(ChessBoardColumnAttribute), false);

            return attributes.Length > 0 ? attributes[0].Value : string.Empty;
        }
    }
}