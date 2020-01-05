namespace GuiStracini.HolidayAPI.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// The country code class.
    /// </summary>
    public class CountryCode
    {
        /// <summary>
        /// Gets or sets the alpha2.
        /// </summary>
        /// <value>
        /// The alpha2.
        /// </value>
        [JsonProperty("alpha-2")]
        public string Alpha2 { get; set; }
        /// <summary>
        /// Gets or sets the alpha3.
        /// </summary>
        /// <value>
        /// The alpha3.
        /// </value>
        [JsonProperty("alpha-3")]
        public string Alpha3 { get; set; }
        /// <summary>
        /// Gets or sets the numeric.
        /// </summary>
        /// <value>
        /// The numeric.
        /// </value>
        [JsonProperty("numeric")]
        public int Numeric { get; set; }

    }
}
