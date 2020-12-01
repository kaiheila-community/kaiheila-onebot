using System;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Events
{
    public static class CqEventHelper
    {
        private const long InitialJavaScriptDateTicks = 621355968000000000;

        private static long ConvertDateToJsTicks(DateTime dateTime)
        {
            TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
            long ret;
            if (dateTime.Kind == DateTimeKind.Utc || dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
                ret = dateTime.Ticks;
            else
            {
                long num = dateTime.Ticks - offset.Ticks;
                if (num > 3155378975999999999L)
                    ret = 3155378975999999999;
                else
                    ret = num < 0L ? 0L : num;
            }

            return ((dateTime.Kind == DateTimeKind.Utc
                ? dateTime.Ticks
                : ret) - InitialJavaScriptDateTicks) / 10000L;
        }

        public static JObject CreateEventObject(CqEventPostType type) =>
            JObject.FromObject(new
            {
                time = ConvertDateToJsTicks(DateTime.UtcNow),
                self_id = 0,
                post_type = type
            });

        public static string GetPostTypeString(CqEventPostType type) =>
            type switch
            {
                CqEventPostType.Message => "message",
                CqEventPostType.Notice => "notice",
                CqEventPostType.Request => "request",
                CqEventPostType.MetaEvent => "meta_event",
                _ => throw new System.NotImplementedException()
            };
    }

    public enum CqEventPostType
    {
        Message = 0,
        Notice = 1,
        Request = 2,
        MetaEvent = 3
    }
}
