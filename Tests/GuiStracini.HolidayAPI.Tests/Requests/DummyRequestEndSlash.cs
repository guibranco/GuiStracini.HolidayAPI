using GuiStracini.HolidayAPI.Transport;
using GuiStracini.HolidayAPI.Utils;

namespace GuiStracini.HolidayAPI.Tests.Requests
{
    /// <summary>
    /// Class DummyRequestEndSlash.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("something/{Dummy}/")]
    public class DummyRequestEndSlash : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>The dummy.</value>
        public string Dummy { get; set; }
    }
}