// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 01-05-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 03-02-2022
// ***********************************************************************
// <copyright file="DummyRequestNoVariable.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using GuiStracini.HolidayAPI.Transport;
using GuiStracini.HolidayAPI.Utils;

namespace GuiStracini.HolidayAPI.Tests.Requests
{
    /// <summary>
    /// Class DummyRequestNoVariable.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    [EndpointRoute("something")]

    public class DummyRequestNoVariable : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>The dummy.</value>
        public string Dummy { get; set; }
    }
}