using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WQH
{
    public class Timestamp
    {
        /// <summary>
        /// 格林威治开始时间
        /// </summary>
        static DateTime uStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// 当前时区开始时间
        /// </summary>
        static DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(uStartTime);
        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DateTime ToDatetime(UInt64 timestamp, string type = "ms")
        {
            switch (type)
            {
                case "ms":
                    return StartTime.AddMilliseconds(timestamp);
                case "s":
                    return StartTime.AddSeconds(timestamp);
                default:
                    throw new Exception("type类型错误");
            }
        }
        public static DateTime ToDatetime(string timestamp, string type = "ms")
        {
            return ToDatetime(UInt64.Parse(timestamp), type);
        }
        /// <summary>
        /// 时间转时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static UInt64 GetTimeStamp(DateTime dt)
        {
            return (UInt64)(dt - StartTime).TotalMilliseconds;
        }
        /// <summary>
        /// 当前时间戳
        /// </summary>
        /// <returns></returns>
        public static UInt64 GetTimeStamp()
        {
            return (UInt64)(DateTime.UtcNow - uStartTime).TotalMilliseconds;
        }

        public static Int64 ToTicks(DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        public static DateTime FromTicks(Int64 ticks)
        {
            TimeSpan toNow = new TimeSpan(ticks * 10000000);
            return StartTime.Add(toNow);
        }
    }
}
