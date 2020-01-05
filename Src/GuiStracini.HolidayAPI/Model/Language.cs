namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// The language class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Model.ILanguage" />
    public class Language : ILanguage
    {
        #region Implementation of ILanguage

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        #endregion
    }
}
