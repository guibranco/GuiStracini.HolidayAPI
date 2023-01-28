// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidayRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    /// <summary>
    /// Class HolidayRequest.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.HolidaySearchableRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.HolidaySearchableRequest" />
    [EndpointRoute("/v1/holidays?key={Key}&country={Country}&year={Year}")]
    internal class HolidayRequest : HolidaySearchableRequest
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }
    }
}
