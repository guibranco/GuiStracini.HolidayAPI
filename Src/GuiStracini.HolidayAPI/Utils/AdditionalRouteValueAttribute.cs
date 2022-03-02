// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 01-05-2022
// ***********************************************************************
// <copyright file="AdditionalRouteValueAttribute.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Utils
{
    using System;

    /// <summary>
    /// Class AdditionalRouteValueAttribute.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class AdditionalRouteValueAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether the additional parameter should be added as query string to the url.
        /// </summary>
        /// <value><c>true</c> if [as query string]; otherwise, <c>false</c>.</value>
        public bool AsQueryString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalRouteValueAttribute" /> class.
        /// </summary>
        /// <param name="asQueryString">if set to <c>true</c> the additional parameter is added in the url as query string.</param>
        public AdditionalRouteValueAttribute(bool asQueryString)
        {
            AsQueryString = asQueryString;
        }
    }
}
