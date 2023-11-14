// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidayResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// Class HolidayResponse.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    public class HolidayResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the holidays.
        /// </summary>
        /// <value>The holidays.</value>
        public IEnumerable<Holiday> Holidays { get; set; }
    }
}
