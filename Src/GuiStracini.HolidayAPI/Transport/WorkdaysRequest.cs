// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-21-2021
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="WorkdaysRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using GuiStracini.HolidayAPI.Utils;

    /// <summary>
    /// Class WorkdaysRequest.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("/v1/workdays?key={Key}&country={Country}&start={Start}&end={End}")]
    internal class WorkdaysRequest : BaseRequest
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
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public string End { get; set; }
    }
}
