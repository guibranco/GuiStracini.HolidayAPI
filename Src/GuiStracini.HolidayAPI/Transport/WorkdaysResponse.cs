// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-21-2021
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="WorkdaysResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    /// <summary>
    /// Class WorkdaysResponse.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    internal class WorkdaysResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the workdays.
        /// </summary>
        /// <value>The workdays.</value>
        public int Workdays { get; set; }
    }
}
