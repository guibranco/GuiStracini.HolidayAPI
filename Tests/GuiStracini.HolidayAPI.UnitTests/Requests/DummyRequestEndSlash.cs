// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 03-02-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 03-02-2022
// ***********************************************************************
// <copyright file="DummyRequestEndSlash.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.UnitTests.Requests
{
    using GuiStracini.HolidayAPI.Transport;
    using GuiStracini.HolidayAPI.Utils;

    /// <summary>
    /// Class DummyRequestEndSlash.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("something/{Dummy}/")]
    public class DummyRequestEndSlash : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>The dummy.</value>
        public string Dummy { get; set; }
    }
}