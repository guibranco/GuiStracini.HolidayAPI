namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    /// <summary>
    /// The countries request class
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("/v1/countries?key={Key}")]
    internal class CountriesRequest : SearchableRequest
    { }
}
