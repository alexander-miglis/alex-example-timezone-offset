using Newtonsoft.Json;
using NodaTime;
using NodaTime.Text;

namespace timezone_offset;

internal class Program
{
    static void Main(string[] args)
    {
        // Parse the markets we have from the markets.json
        // https://github.com/ILC-Technology/martech-toolkit/blob/main/libs/env-utils/src/lib/markets/markets.json
        MarketsModel marketsData;
        using (StreamReader file = File.OpenText(@"markets.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            marketsData = (MarketsModel)serializer.Deserialize(file, objectType: typeof(MarketsModel))!;
        }


        string marketCode = "se";
        Market? market = marketsData.Markets.FirstOrDefault(m => m.MarketCode == marketCode);

        DateTime utcTime = ConvertToUtc("2023-06-29", "10:00:00", market.TimeZone);
    }

    public static DateTime ConvertToUtc(string dateString, string timeString, string timeZoneId)
    {
        try
        {
            // Parse the date and time strings
            LocalDate date = LocalDatePattern.Iso.Parse(dateString).Value;
            LocalTime time = LocalTimePattern.ExtendedIso.Parse(timeString).Value;

            // Combine the date and time into a LocalDateTime
            LocalDateTime localDateTime = date + time;

            return ConvertToUtc(localDateTime, timeZoneId);
        }
        catch (Exception)
        {
            // If there is an error or the timezone is not found, you can handle it as per your requirements
            throw new ArgumentException("Bad date/time or timezone");
        }
    }

    public static DateTime ConvertToUtc(LocalDateTime localDateTime, string timeZoneId)
    {
        try
        {
            // Load the NodaTime timezone provider
            var tzdb = DateTimeZoneProviders.Tzdb;

            // Get the NodaTime timezone for the provided timezone ID
            var timeZone = tzdb.GetZoneOrNull(timeZoneId);

            if (timeZone != null)
            {

                // Create a ZonedDateTime object by combining the LocalDateTime and the TimeZone
                var zonedDateTime = localDateTime.InZoneStrictly(timeZone);

                // Convert the ZonedDateTime to an Instant representing the corresponding UTC time
                var instant = zonedDateTime.ToInstant();

                // Convert the Instant to a DateTime in UTC
                var utcDateTime = instant.ToDateTimeUtc();

                return utcDateTime;
            }
        }
        catch (Exception)
        {
            // If there is an error or the timezone is not found, you can handle it as per your requirements
        }

        // If the timezone ID is not found, return the original DateTime value
        return localDateTime.ToDateTimeUnspecified();
    }

}