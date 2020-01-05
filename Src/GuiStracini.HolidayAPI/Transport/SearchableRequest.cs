namespace GuiStracini.HolidayAPI.Transport
{
    /// <summary>
    /// The searchable request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    public abstract class SearchableRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>
        /// The search.
        /// </value>
        public string Search { get; set; }
    }
}
