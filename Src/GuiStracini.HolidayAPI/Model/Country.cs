// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="Country.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// The country class.
    /// </summary>
    public class Country : ICountry
    {
        #region Implementation of ICountry

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public string[] Languages { get; set; }

        /// <summary>
        /// Gets the codes.
        /// </summary>
        /// <value>The codes.</value>
        public CountryCode Codes { get; set; }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>The flag.</value>
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets the subdivisions.
        /// </summary>
        /// <value>The subdivisions.</value>
        public Subdivision[] Subdivisions { get; set; }

        #endregion
    }
}

