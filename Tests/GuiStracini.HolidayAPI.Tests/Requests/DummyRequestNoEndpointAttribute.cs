using GuiStracini.HolidayAPI.Transport;

namespace GuiStracini.HolidayAPI.Tests.Requests
{
    /// <summary>
    /// Class DummyInvalidRequest.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    public class DummyRequestNoEndpointAttribute : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>
        /// The dummy.
        /// </value>
        public string Dummy { get; set; }
    }
}