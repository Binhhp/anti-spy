using Newtonsoft.Json;
using System.Collections.Generic;

namespace WixSharp.Entities.BusinessInfo
{
    public class SiteProperties
    {
        [JsonProperty("categories")]
        public CategoriesMemeber Categories { get; set; }

        [JsonProperty("locale")]
        public LocaleMember Locale { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("paymentCurrency")]
        public string PaymentCurrency { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("address")]
        public AddressMember Address { get; set; }

        [JsonProperty("siteDisplayName")]
        public string SiteDisplayName { get; set; }

        [JsonProperty("businessName")]
        public string BusinessName { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("businessSchedule")]
        public BusinessScheduleMember BusinessSchedule { get; set; }

        [JsonProperty("multilingual")]
        public object Multilingual { get; set; }

        [JsonProperty("consentPolicy")]
        public ConsentPolicyMember ConsentPolicy { get; set; }

        [JsonProperty("businessConfig")]
        public string BusinessConfig { get; set; }
    }

    public class CategoriesMemeber
    {
        [JsonProperty("primary")]
        public string Primary { get; set; }

        [JsonProperty("secondary")]
        public IEnumerable<string> Secondary { get; set; }
    }
    public class LocaleMember
    {

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
    public class AddressMember
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("hint")]
        public HintMember Hint { get; set; }

        [JsonProperty("isPhysical")]
        public bool IsPhysical { get; set; }

        [JsonProperty("googleFormattedAddress")]
        public string GoogleFormattedAddress { get; set; }

        [JsonProperty("streetNumber")]
        public string StreetNumber { get; set; }

        [JsonProperty("apartmentNumber")]
        public string ApartmentNumber { get; set; }
        [JsonProperty("coordinates")]
        public CoordinatesMember Coordinates { get; set; }
    }
    public class HintMember
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("placement")]
        public string Placement { get; set; }

    }
    public class CoordinatesMember
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }
    public class BusinessScheduleMember
    {
        [JsonProperty("periods")]
        public List<PeriodsMember> Periods { get; set; }

        [JsonProperty("specialHourPeriod")]
        public List<SpecialHourPeriodMember> SpecialHourPeriod { get; set; }
    }
    public class PeriodsMember
    {
        [JsonProperty("openDay")]
        public string OpenDay { get; set; }

        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("closeDay")]
        public string CloseDay { get; set; }

        [JsonProperty("closeTime")]
        public string CloseTime { get; set; }
    }
    public class SpecialHourPeriodMember
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("endDate")]
        public string EndDate { get; set; }

        [JsonProperty("isClosed")]
        public bool IsClosed { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
    public class MultilingualMember
    {
        [JsonProperty("supportedLanguages")]
        public SupportedLanguagesMember SupportedLanguages { get; set; }

        [JsonProperty("autoRedirect")]
        public bool AutoRedirect { get; set; }
    }
    public class SupportedLanguagesMember
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("locale")]
        public LocaleMember Locale { get; set; }

        [JsonProperty("isPrimary")]
        public bool IsPrimary { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("resolutionMethod")]
        public string ResolutionMethod { get; set; }
    }
    public class ConsentPolicyMember
    {
        [JsonProperty("essential")]
        public bool Essential { get; set; }

        [JsonProperty("functional")]
        public bool Functional { get; set; }

        [JsonProperty("analytics")]
        public bool Analytics { get; set; }

        [JsonProperty("advertising")]
        public bool Advertising { get; set; }

        [JsonProperty("dataToThirdParty")]
        public bool DataToThirdParty { get; set; }
    }
}
