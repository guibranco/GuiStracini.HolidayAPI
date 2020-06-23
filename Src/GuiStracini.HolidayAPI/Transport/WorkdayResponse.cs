// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="WorkdayResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using GuiStracini.HolidayAPI.Model;

namespace GuiStracini.HolidayAPI.Transport
{
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
