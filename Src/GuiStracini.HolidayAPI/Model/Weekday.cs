namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// The weekday class.
    /// </summary>
    public class Weekday
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public WeekdayDetail Date { get; set; }
        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public WeekdayDetail Observed { get; set; }
    }
}
