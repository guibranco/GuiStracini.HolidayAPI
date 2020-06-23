// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="WorkdayRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using GuiStracini.HolidayAPI.Utils;

namespace GuiStracini.HolidayAPI.Transport
{
    /// <summary>
    /// Class WorkdayRequest.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("/v1/workday?key={Key}&country={Country}&start={Start}&days={Days}")]
    internal class WorkdayRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>The days.</value>
        public int Days { get; set; }
    }
}
