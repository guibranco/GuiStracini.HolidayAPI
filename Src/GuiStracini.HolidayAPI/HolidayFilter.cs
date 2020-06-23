// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="HolidayFilter.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI
{
    /// <summary>
    /// Class HolidayFilter.
    /// </summary>
    public class HolidayFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayFilter" /> class.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="year">The year.</param>
        public HolidayFilter(string country, int year)
        {
            Country = country;
            Year = year;
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public int? Month { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>The day.</value>
        public int? Day { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HolidayFilter" /> is previous.
        /// </summary>
        /// <value><c>true</c> if previous; otherwise, <c>false</c>.</value>
        public bool? Previous { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HolidayFilter" /> is upcoming.
        /// </summary>
        /// <value><c>true</c> if upcoming; otherwise, <c>false</c>.</value>
        public bool? Upcoming { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HolidayFilter" /> is public.
        /// </summary>
        /// <value><c>true</c> if public; otherwise, <c>false</c>.</value>
        public bool? Public { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HolidayFilter" /> is subdivisions.
        /// </summary>
        /// <value><c>true</c> if subdivisions; otherwise, <c>false</c>.</value>
        public bool? Subdivisions { get; set; }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>The search.</value>
        public string Search { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }
    }
}
