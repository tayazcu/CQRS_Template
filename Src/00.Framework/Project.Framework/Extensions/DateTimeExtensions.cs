using System;
using System.Globalization;

namespace Project.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateStringShahmsi(this DateTime value)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string year = persianCalendar.GetYear(value).ToString();
            string month = persianCalendar.GetMonth(value).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(value).ToString().PadLeft(2, '0');

            return $"{year}/{month}/{day}";
        }
        public static string ToShortDateStringShahmsi(this DateTime? value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return null;

            PersianCalendar persianCalendar = new PersianCalendar();

            DateTime dt = Convert.ToDateTime(value.ToString());
            string year = persianCalendar.GetYear(dt).ToString();
            string month = persianCalendar.GetMonth(dt).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(dt).ToString().PadLeft(2, '0');

            return $"{year}/{month}/{day}";
        }

        public static string ToStringShahmsi(this DateTime value)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string year = persianCalendar.GetYear(value).ToString();
            string month = persianCalendar.GetMonth(value).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(value).ToString().PadLeft(2, '0');

            string hour = value.Hour.ToString().PadLeft(2, '0');
            string minute = value.Minute.ToString().PadLeft(2, '0');
            string second = value.Second.ToString().PadLeft(2, '0');

            return $"{year}/{month}/{day} {hour}:{minute}:{second}";
        }
    }
}
