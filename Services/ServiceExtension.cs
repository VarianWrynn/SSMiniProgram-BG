using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public static class ServiceExtension
    {

        /// <summary>
        /// 当前值如果是null，则返回0，否则返回当前值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ConvertToNotNull(this Nullable<int> val)
        {
            return val.HasValue ? val.Value : 0;
        }

        public static long ConvertToNotNull(this Nullable<long> val)
        {
            return val.HasValue ? val.Value : 0;
        }

        public static double ConvertToNotNull(this Nullable<double> val)
        {
            return val.HasValue ? val.Value : 0;
        }

        public static decimal ConvertToNotNull(this Nullable<decimal> val)
        {
            return val.HasValue ? val.Value : 0;
        }

        /// <summary>
        /// 设置bool的扩展方法，如果是null,则返回false
        /// CreateDate:2013年11月21日18:46:50
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ConvertToNotNull(this Nullable<bool> val)
        {
            return val.HasValue ? val.Value : false;
        }

        /// <summary>
        /// 对字符串时间扩展，字符串时间调用该方法可以转化为yyyy-MM-dd HH:mm:ss格式的时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Nullable<DateTime> ConvertToDate(this string date)
        {
            if (date == "" || date == null)
                return null;
            return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
        }

        /// <summary>
        /// 对Nullabe《Datetime》的时间格式扩展，，调用该方法可以转为yyyy-MM-dd HH:mm:ss的字符串时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertToString(this Nullable<DateTime> date)
        {
            if (date == null)
                return string.Empty;
            return date.HasValue ? date.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        }
    }
}
