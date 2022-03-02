// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 01-05-2022
// ***********************************************************************
// <copyright file="SearchableRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using GuiStracini.HolidayAPI.Utils;
    using Newtonsoft.Json;

    /// <summary>
    /// The searchable request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    internal abstract class SearchableRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>The search.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("search")]
        [JsonIgnore]
        public string Search { get; set; }
    }
}
