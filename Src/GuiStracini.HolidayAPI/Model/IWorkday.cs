// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="IWorkday.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// Interface IWorkday
    /// </summary>
    public interface IWorkday
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the weekday.
        /// </summary>
        /// <value>The weekday.</value>
        WeekdayDetail Weekday { get; set; }
    }
}
