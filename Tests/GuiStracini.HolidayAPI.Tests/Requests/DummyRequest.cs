// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 03-02-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 03-02-2022
// ***********************************************************************
// <copyright file="DummyRequest.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.UnitTests.Requests
{
    using GuiStracini.HolidayAPI.Transport;
    using GuiStracini.HolidayAPI.Utils;
    using Newtonsoft.Json;

    /// <summary>
    /// Dummy request for test purposes.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("something/{Dummy}")]
    public class DummyRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>The dummy.</value>
        public string Dummy { get; set; }

        /// <summary>
        /// Gets or sets the foo.
        /// </summary>
        /// <value>The foo.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("foo")]
        [JsonIgnore]
        public string Foo { get; set; }

        /// <summary>
        /// Gets or sets the foo bar.
        /// </summary>
        /// <value>The foo bar.</value>
        [AdditionalRouteValue(true)]
        [JsonProperty("fooBar")]
        [JsonIgnore]
        public bool? FooBar { get; set; }
    }
}