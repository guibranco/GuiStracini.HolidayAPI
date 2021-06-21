// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="Workday.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    using System;

    /// <summary>
    /// Class Workday.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Model.IWorkday" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Model.IWorkday" />
    public class Workday : IWorkday
    {
        #region Implementation of IWorkday

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the weekday.
        /// </summary>
        /// <value>The weekday.</value>
        public WeekdayDetail Weekday { get; set; }

        #endregion
    }
}
