namespace GuiStracini.HolidayAPI.Transport
{
    using Model;

    /// <summary>
    /// The base response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Model.IResponseMetadata" />
    public class BaseResponse : IResponseMetadata
    {
        #region Implementation of IResponseMetadata

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>
        /// The requests.
        /// </value>
        public RequestMetadata Requests { get; set; }

        #endregion
    }
}
