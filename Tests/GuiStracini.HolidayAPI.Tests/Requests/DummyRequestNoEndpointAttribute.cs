﻿// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 03-02-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 03-02-2022
// ***********************************************************************
// <copyright file="DummyRequestNoEndpointAttribute.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using GuiStracini.HolidayAPI.Transport;

namespace GuiStracini.HolidayAPI.Tests.Requests
{
    /// <summary>
    /// Class DummyInvalidRequest.
    /// Implements the <see cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseRequest" />
    public class DummyRequestNoEndpointAttribute : BaseRequest
    {
        /// <summary>
        /// Gets or sets the dummy.
        /// </summary>
        /// <value>The dummy.</value>
        public string Dummy { get; set; }
    }
}