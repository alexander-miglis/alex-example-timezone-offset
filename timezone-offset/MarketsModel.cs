using Newtonsoft.Json;

namespace timezone_offset;

public class MarketsModel
{
    [JsonProperty("markets")]
    public List<Market> Markets { get; set; }

    public MarketsModel()
    {
        Markets = new List<Market>();
    }
}

public class Market
{
    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("locale")]
    public string Locale { get; set; }

    [JsonProperty("timeZone")]
    public string TimeZone { get; set; }

    [JsonProperty("marketCode")]
    public string MarketCode { get; set; }

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    [JsonProperty("countryName")]
    public string CountryName { get; set; }

    [JsonProperty("cultureCode")]
    public string CultureCode { get; set; }

    [JsonProperty("languageCode")]
    public string LanguageCode { get; set; }

    [JsonProperty("languageName")]
    public string LanguageName { get; set; }

    [JsonProperty("countryNameLocale")]
    public string CountryNameLocale { get; set; }

    [JsonProperty("urlLanguageFolder")]
    public string UlLanguageFolder { get; set; }

    [JsonProperty("available_products")]
    public string[] AvailableProducts { get; set; }

    [JsonProperty("secondaryCultureCode")]
    public string SecondaryCultureCode { get; set; }

    [JsonProperty("qaDomain")]
    public string QaDomain { get; set; }

    [JsonProperty("liveDomain")]
    public string LiveDomain { get; set; }
}