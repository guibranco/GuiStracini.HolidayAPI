namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    [EndpointRoute("/v1/holidays")]
    internal class HolidayRequest : BaseRequest
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
