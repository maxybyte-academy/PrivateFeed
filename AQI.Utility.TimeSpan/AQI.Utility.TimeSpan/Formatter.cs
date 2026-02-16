namespace AQI.Utility.TimeSpan
{
    public static class Formatter
    {

        public static string ToRelativeTimeAgoString(this DateTime occurred)
        {
            var timeSpan = DateTime.UtcNow - occurred.ToUniversalTime();

            if (timeSpan.TotalSeconds < 60)
                return $"{(int)timeSpan.TotalSeconds} second{((int)timeSpan.TotalSeconds != 1 ? "s" : "")} ago";

            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} minute{((int)timeSpan.TotalMinutes != 1 ? "s" : "")} ago";

            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} hour{((int)timeSpan.TotalHours != 1 ? "s" : "")} ago";

            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} day{((int)timeSpan.TotalDays != 1 ? "s" : "")} ago";

            if (timeSpan.TotalDays < 365)
            {
                int months = (int)(timeSpan.TotalDays / 30);
                return $"{months} month{(months != 1 ? "s" : "")} ago";
            }

            int years = (int)(timeSpan.TotalDays / 365);
            return $"{years} year{(years != 1 ? "s" : "")} ago";
        }
    }
}
