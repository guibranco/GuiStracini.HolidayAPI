// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="IHoliday.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    using System;

    /// <summary>
    /// The holiday interface.
    /// </summary>
    public interface IHoliday
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>The date.</value>
        DateTime Date { get; set; }

        /// <summary>
        /// Gets the observed.
        /// </summary>
        /// <value>The observed.</value>
        DateTime Observed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IHoliday" /> is public.
        /// </summary>
        /// <value><c>true</c> if public; otherwise, <c>false</c>.</value>
        bool Public { get; set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>The country.</value>
        string Country { get; set; }

        /// <summary>
        /// Gets the UUID.
        /// </summary>
        /// <value>The UUID.</value>
        Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the weekday.
        /// </summary>
        /// <value>The weekday.</value>
        Weekday Weekday { get; set; }
    }
}
