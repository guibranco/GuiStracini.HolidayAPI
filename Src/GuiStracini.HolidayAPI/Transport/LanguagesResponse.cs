// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="LanguagesResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using Model;
    using System.Collections.Generic;

    /// <summary>
    /// The languages response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.BaseResponse" />
    public class LanguagesResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public IEnumerable<Language> Languages { get; set; }
    }
}
