namespace GuiStracini.HolidayAPI.Model
{
    using System;

    public class Holiday : IHoliday
    {
        #region Implementation of IHoliday

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTime Observed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IHoliday"/> is public.
        /// </summary>
        /// <value>
        ///   <c>true</c> if public; otherwise, <c>false</c>.
        /// </value>
        public bool Public { get; set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets the UUID.
        /// </summary>
        /// <value>
        /// The UUID.
        /// </value>
        public Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the weekday.
        /// </summary>
        /// <value>
        /// The weekday.
        /// </value>
        public Weekday Weekday { get; set; }

        #endregion
    }
}
