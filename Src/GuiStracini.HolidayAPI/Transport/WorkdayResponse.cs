// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="WorkdayResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace GuiStracini.HolidayAPI.Transport
{
    using GuiStracini.HolidayAPI.Model;

    /// <summary>
    /// Class WorkdayResponse.
    /// </summary>
    internal class WorkdayResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the workday.
        /// </summary>
        /// <value>The workday.</value>
        public Workday Workday { get; set; }
    }
}
