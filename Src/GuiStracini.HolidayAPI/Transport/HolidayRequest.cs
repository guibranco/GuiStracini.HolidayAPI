namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    [EndpointRoute("/v1/holidays?key={Key}&country={Country}&year={Year}")]
    internal class HolidayRequest : HolidaySearchableRequest
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }
    }
}
