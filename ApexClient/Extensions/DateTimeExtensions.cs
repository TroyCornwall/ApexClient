namespace Library.Extensions;

public static class DateTimeExtensions
{
    private static TimeZoneInfo? _nzTimeZone;
    private static TimeZoneInfo GetNzTimeZone()
    {
        if (_nzTimeZone != null) return _nzTimeZone;
        // Need to check both IANA and Microsoft timezones for Windows/Linux compatibility        
        _nzTimeZone = TimeZoneInfo
            .GetSystemTimeZones()
            .SingleOrDefault(info => info.Id == "Pacific/Auckland" || info.Id == "New Zealand Standard Time");

        if (_nzTimeZone == null)
            throw new Exception("Cannot find NZ Time Zone on the server.");

        return _nzTimeZone;
    }
    
    public static DateTimeOffset ToNzDateTimeOffSet(this DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified)
        {
            return new DateTimeOffset(dateTime, GetNzTimeZone().GetUtcOffset(dateTime));
        }

        var nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), GetNzTimeZone());
        var nzDateTimeOffset = new DateTimeOffset(nzDateTime, GetNzTimeZone().GetUtcOffset(nzDateTime));
        return nzDateTimeOffset;
    }
    
    public static DateTimeOffset ToNzDateTimeOffSet(this DateTimeOffset dateTimeOffset)
    {
        return TimeZoneInfo.ConvertTime(dateTimeOffset, GetNzTimeZone());
    }
}