namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// The response metadata interface.
    /// </summary>
    public interface IResponseMetadata
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        int Status { get; set; }

        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        RequestMetadata Requests { get; set; }
    }
}
