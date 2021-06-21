// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="IHolidayAPIClient.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace GuiStracini.HolidayAPI
{
    using System;
    using GuiStracini.HolidayAPI.Model;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IHolidayApiClient
    /// </summary>
    public interface IHolidayApiClient
    {
        /// <summary>
        /// Gets the usage data.
        /// </summary>
        /// <value>The usage data.</value>
        RequestMetadata UsageData { get; }

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="year">The year.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IHoliday&gt;&gt;.</returns>
        Task<IEnumerable<IHoliday>> GetHolidaysAsync(string country, int year, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IHoliday&gt;&gt;.</returns>
        Task<IEnumerable<IHoliday>> GetHolidaysAsync(HolidayFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;ICountry&gt;&gt;.</returns>
        Task<IEnumerable<ICountry>> GetCountriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;ICountry&gt;&gt;.</returns>
        Task<IEnumerable<ICountry>> GetCountriesAsync(string search, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;ILanguage&gt;&gt;.</returns>
        Task<IEnumerable<ILanguage>> GetLanguagesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;IEnumerable&lt;ILanguage&gt;&gt;.</returns>
        Task<IEnumerable<ILanguage>> GetLanguagesAsync(string search, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the workday asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="start">The start.</param>
        /// <param name="days">The days.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;Workday&gt;.</returns>
        Task<Workday> GetWorkdayAsync(string country, DateTime start, int days, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the workdays asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;int&gt;.</returns>
        Task<int> GetWorkdaysAsync(string country, DateTime start, DateTime end, CancellationToken cancellationToken);
    }
}
