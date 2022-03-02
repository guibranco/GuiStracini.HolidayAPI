// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 01-05-2022
// ***********************************************************************
// <copyright file="LanguagesRequest.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using Utils;

    /// <summary>
    /// The languages request class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Transport.SearchableRequest" />
    [EndpointRoute("v1/languages?key={Key}")]
    internal class LanguagesRequest : SearchableRequest
    { }
}
