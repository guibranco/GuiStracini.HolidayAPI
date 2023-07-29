// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidaySearchableRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using GuiStracini.HolidayAPI.Utils;
    using Newtonsoft.Json;

    /// <summary>
    /// The holiday searchable request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.SearchableRequest" />
    internal abstract class HolidaySearchableRequest : SearchableRequest
    {
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("month")]
        [JsonIgnore]
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("day")]
        [JsonIgnore]
        public int? Day { get; set; }

        /// <summary>
        /// Gets or sets the previous.
        /// </summary>
        /// <value>The previous.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("previous")]
        [JsonIgnore]
        public bool? Previous { get; set; }

        /// <summary>
        /// Gets or sets the upcoming.
        /// </summary>
        /// <value>The upcoming.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("upcoming")]
        [JsonIgnore]
        public bool? Upcoming { get; set; }

        /// <summary>
        /// Gets or sets the public.
        /// </summary>
        /// <value>The public.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("public")]
        [JsonIgnore]
        public bool? Public { get; set; }

        /// <summary>
        /// Gets or sets the subdivisions.
        /// </summary>
        /// <value>The subdivisions.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("subdivisions")]
        [JsonIgnore]
        public bool? Subdivisions { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("language")]
        [JsonIgnore]
        public string Language { get; set; }
    }
}
