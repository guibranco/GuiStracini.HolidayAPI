﻿// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="CountriesResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using System.Collections.Generic;
    using Model;

    /// <summary>
    /// The countries response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    public class CountriesResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        /// <value>The countries.</value>
        public IEnumerable<Country> Countries { get; set; }
    }
}
