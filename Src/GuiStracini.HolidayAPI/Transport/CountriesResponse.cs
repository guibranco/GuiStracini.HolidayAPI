namespace GuiStracini.HolidayAPI.Transport
{
    using Model;
    using System.Collections.Generic;

    /// <summary>
    /// The countries response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    public class CountriesResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        public IEnumerable<Country> Countries { get; set; }
    }
}
