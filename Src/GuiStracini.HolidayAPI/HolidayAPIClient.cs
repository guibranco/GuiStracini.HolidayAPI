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
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using GuiStracini.HolidayAPI.GoodPractices;
    using GuiStracini.HolidayAPI.Model;
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
                Message = "Make at least on request before get the metadata",
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
        private async ValueTask<TResponse> Execute<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken
        )
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            var response = await _serviceFactory
                .Post<TRequest, TResponse>(request, cancellationToken)
                .ConfigureAwait(false);

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
        /// Asynchronously retrieves a list of holidays based on the specified filter criteria.
        /// </summary>
        /// <param name="filter">An instance of <see cref="HolidayFilter"/> containing the criteria for filtering holidays.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see cref="IHoliday"/> objects representing the holidays.</returns>
        /// <remarks>
        /// This method constructs a <see cref="HolidayRequest"/> using the provided filter parameters, including country, year, month, day, language, and other optional fields.
        /// It then sends this request to an external service to fetch the holiday data asynchronously.
        /// The method will return the list of holidays contained in the response from the external service.
        /// If the operation is canceled before completion, the cancellation token will be triggered, and an appropriate exception will be thrown.
        /// </remarks>
        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(
            string country,
            int year,
            CancellationToken cancellationToken
        )
        {
            return await GetHolidaysAsync(new HolidayFilter(country, year), cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;IHoliday&gt;.</returns>
        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(
            HolidayFilter filter,
            CancellationToken cancellationToken
        )
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
                Upcoming = filter.Upcoming,
            };
            var response = await Execute<HolidayRequest, HolidayResponse>(
                    request,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return response.Holidays;
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ICountry&gt;.</returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(
            CancellationToken cancellationToken
        )
        {
            return await GetCountriesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ICountry&gt;.</returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(
            string search,
            CancellationToken cancellationToken
        )
        {
            var request = new CountriesRequest { Key = _apiKey };

            if (!string.IsNullOrWhiteSpace(search))
            {
                request.Search = search;
            }

            var response = await Execute<CountriesRequest, CountriesResponse>(
                    request,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return response.Countries;
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ILanguage&gt;.</returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(
            CancellationToken cancellationToken
        )
        {
            return await GetLanguagesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IEnumerable&lt;ILanguage&gt;.</returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(
            string search,
            CancellationToken cancellationToken
        )
        {
            var request = new LanguagesRequest { Key = _apiKey };
            if (!string.IsNullOrWhiteSpace(search))
            {
                request.Search = search;
            }

            var response = await Execute<LanguagesRequest, LanguagesResponse>(
                    request,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return response.Languages;
        }

        /// <summary>
        /// Asynchronously retrieves the workday information for a specified country and date range.
        /// </summary>
        /// <param name="country">The country for which to retrieve the workday information.</param>
        /// <param name="start">The starting date from which to calculate the workdays.</param>
        /// <param name="days">The number of workdays to retrieve.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation if needed.</param>
        /// <returns>A task that represents the asynchronous operation, containing the workday information for the specified parameters.</returns>
        /// <remarks>
        /// This method constructs a request to fetch workday data based on the provided country, start date, and number of days.
        /// It sends the request asynchronously and awaits the response. The response is expected to contain a Workday object,
        /// which encapsulates the workday details for the specified parameters. If the operation is canceled via the
        /// <paramref name="cancellationToken"/>, an OperationCanceledException may be thrown.
        /// </remarks>
        public async Task<Workday> GetWorkdayAsync(
            string country,
            DateTime start,
            int days,
            CancellationToken cancellationToken
        )
        {
            var request = new WorkdayRequest
            {
                Country = country,
                Days = days,
                Key = _apiKey,
                Start = start.ToString("yyyy-MM-dd"),
            };
            var response = await Execute<WorkdayRequest, WorkdayResponse>(
                    request,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return response.Workday;
        }

        /// <summary>
        /// Asynchronously retrieves the number of workdays between two dates for a specified country.
        /// </summary>
        /// <param name="country">The country code for which to retrieve workdays.</param>
        /// <param name="start">The start date for the workdays calculation.</param>
        /// <param name="end">The end date for the workdays calculation.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of workdays between the specified start and end dates.</returns>
        /// <remarks>
        /// This method constructs a request to an external service to calculate the number of workdays based on the provided country and date range.
        /// It formats the start and end dates to the "yyyy-MM-dd" format required by the service.
        /// The method uses an asynchronous pattern to execute the request and retrieve the response, ensuring that it does not block the calling thread.
        /// The result is extracted from the response and returned as an integer representing the total workdays within the specified period.
        /// </remarks>
        public async Task<int> GetWorkdaysAsync(
            string country,
            DateTime start,
            DateTime end,
            CancellationToken cancellationToken
        )
        {
            var request = new WorkdaysRequest
            {
                Country = country,
                End = end.ToString("yyyy-MM-dd"),
                Key = _apiKey,
                Start = start.ToString("yyyy-MM-dd"),
            };
            var response = await Execute<WorkdaysRequest, WorkdaysResponse>(
                    request,
                    cancellationToken
                )
                .ConfigureAwait(false);
            return response.Workdays;
        }

        #endregion
    }
}
