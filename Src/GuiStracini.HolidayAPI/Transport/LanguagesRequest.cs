namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    /// <summary>
    /// The languages request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.SearchableRequest" />
    [EndpointRoute("v1/languages?key={Key}")]
    public class LanguagesRequest : SearchableRequest
    { }
}
