using GuiStracini.HolidayAPI.Utils;
using Newtonsoft.Json;

namespace GuiStracini.HolidayAPI.Transport
{
    /// <summary>
    /// The searchable request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    internal abstract class SearchableRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        [AdditionalRouteValue(true)]
        [JsonProperty("search")]
        [JsonIgnore]
        public string Search { get; set; }
    }
}
