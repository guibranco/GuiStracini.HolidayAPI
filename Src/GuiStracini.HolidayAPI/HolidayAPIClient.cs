// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidayAPIClient.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI
{
    using GuiStracini.HolidayAPI.GoodPractices;
    using System.Net.Http;
    using GuiStracini.HolidayAPI.Model;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GuiStracini.HolidayAPI.Transport;
    using GuiStracini.HolidayAPI.Utils;

    /// <summary>
    /// Class HolidayApiClient.
    /// Implements the <see cref="GuiStracini.HolidayAPI.IHolidayApiClient" />
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.IHolidayApiClient" />
    public class HolidayApiClient : IHolidayApiClient
    {
        /// <summary>
        /// The API key
        /// </summary>
        private readonly Guid _apiKey;

        /// <summary>
        /// The metadata
        /// </summary>
        private RequestMetadata _metadata;

        /// <summary>
        /// The service factory
        /// </summary>
        private readonly ServiceFactory _serviceFactory;

        #region ~Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayApiClient" /> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public HolidayApiClient(string apiKey, HttpClient httpClient)
        {
            _apiKey = new Guid(apiKey);
            _metadata = new RequestMetadata
            {
                Message = "Make at least on request before get the metadata"
            };
            _serviceFactory = new ServiceFactory(httpClient);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>TResponse.</returns>
        /// <exception cref="GuiStracini.HolidayAPI.GoodPractices.HolidayApiException"></exception>
        private async ValueTask<TResponse> Execute<TRequest, TResponse>(TRequest request,
            CancellationToken cancellationToken) where TRequest : BaseRequest where TResponse : BaseResponse
        {
            var response = await _serviceFactory.Post<TRequest, TResponse>(request, cancellationToken).ConfigureAwait(false);

            _metadata = response.Requests ?? new RequestMetadata();
            _metadata.Warning = response.Warning;
            _metadata.LastCall = DateTime.Now;
            if (response.Status == 200)
            {
                _metadata.Message = "Success";
                return response;
            }
            _metadata.Message = $"Error code: {response.Status}";
            throw new HolidayApiException(response.Status, response.Error ?? response.Warning);
        }

        #endregion

        #region Implementation of IHolidayAPIClient

        /// <summary>
        /// Gets the usage data.
        /// </summary>
        /// <value>The usage data.</value>
        public RequestMetadata UsageData => _metadata;

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="year">The year.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;IHoliday&gt;.</returns>
        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(string country, int year, CancellationToken cancellationToken)
        {
            return await GetHolidaysAsync(new HolidayFilter(country, year), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;IHoliday&gt;.</returns>
        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(HolidayFilter filter, CancellationToken cancellationToken)
        {
            var request = new HolidayRequest
            {
                Key = _apiKey,
                Country = filter.Country,
                Year = filter.Year,
                Day = filter.Day,
                Month = filter.Month,
                Language = filter.Language,
                Previous = filter.Previous,
                Public = filter.Public,
                Search = filter.Search,
                Subdivisions = filter.Subdivisions,
                Upcoming = filter.Upcoming
            };
            var response = await Execute<HolidayRequest, HolidayResponse>(request, cancellationToken).ConfigureAwait(false);
            return response.Holidays;
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ICountry&gt;.</returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(CancellationToken cancellationToken)
        {
            return await GetCountriesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ICountry&gt;.</returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(string search, CancellationToken cancellationToken)
        {
            var request = new CountriesRequest { Key = _apiKey };

            if (!string.IsNullOrWhiteSpace(search))
            {
                request.Search = search;
            }

            var response = await Execute<CountriesRequest, CountriesResponse>(request, cancellationToken).ConfigureAwait(false);
            return response.Countries;
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ILanguage&gt;.</returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(CancellationToken cancellationToken)
        {
            return await GetLanguagesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ILanguage&gt;.</returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(string search, CancellationToken cancellationToken)
        {
            var request = new LanguagesRequest { Key = _apiKey };
            if (!string.IsNullOrWhiteSpace(search))
            {
                request.Search = search;
            }

            var response = await Execute<LanguagesRequest, LanguagesResponse>(request, cancellationToken).ConfigureAwait(false);
            return response.Languages;
        }

        /// <summary>
        /// get workday as an asynchronous operation.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="start">The start.</param>
        /// <param name="days">The days.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;Workday&gt;.</returns>
        public async Task<Workday> GetWorkdayAsync(string country, DateTime start, int days,
            CancellationToken cancellationToken)
        {
            var request = new WorkdayRequest
            {
                Country = country,
                Days = days,
                Key = _apiKey,
                Start = start.ToString("yyyy-MM-dd")
            };
            var response = await Execute<WorkdayRequest, WorkdayResponse>(request, cancellationToken).ConfigureAwait(false);
            return response.Workday;
        }

        /// <summary>
        /// get workdays as an asynchronous operation.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;int&gt; representing the asynchronous operation.</returns>
        public async Task<int> GetWorkdaysAsync(string country, DateTime start, DateTime end,
            CancellationToken cancellationToken)
        {
            var request = new WorkdaysRequest
            {
                Country = country,
                End = end.ToString("yyyy-MM-dd"),
                Key = _apiKey,
                Start = start.ToString("yyyy-MM-dd")
            };
            var response = await Execute<WorkdaysRequest, WorkdaysResponse>(request, cancellationToken).ConfigureAwait(false);
            return response.Workdays;
        }

        #endregion
    }
}
