// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 01-05-2022
// ***********************************************************************
// <copyright file="Holiday.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    using System;

    /// <summary>
    /// Class Holiday.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Model.IHoliday" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Model.IHoliday" />
    public class Holiday : IHoliday
    {
        #region Implementation of IHoliday

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the observed.
        /// </summary>
        /// <value>The observed.</value>
        public DateTime Observed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IHoliday" /> is public.
        /// </summary>
        /// <value><c>true</c> if public; otherwise, <c>false</c>.</value>
        public bool Public { get; set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets the UUID.
        /// </summary>
        /// <value>The UUID.</value>
        public Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the weekday.
        /// </summary>
        /// <value>The weekday.</value>
        public Weekday Weekday { get; set; }

        #endregion
    }
}
