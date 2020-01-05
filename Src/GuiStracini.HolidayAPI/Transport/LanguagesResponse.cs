namespace GuiStracini.HolidayAPI.Transport
{
    using Model;
    using System.Collections.Generic;

    /// <summary>
    /// The languages response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    public class LanguagesResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public IEnumerable<Language> Languages { get; set; }
    }
}
