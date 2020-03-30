using GuiStracini.HolidayAPI.GoodPractices;
using System.Net.Http;

namespace GuiStracini.HolidayAPI
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Transport;
    using Utils;

    public class HolidayAPIClient : IHolidayAPIClient
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
        /// Initializes a new instance of the <see cref="HolidayAPIClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public HolidayAPIClient(string apiKey, HttpClient httpClient)
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
        /// <returns></returns>
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
            throw new HolidayAPIException(response.Status, response.Error ?? response.Warning);
        }

        #endregion

        #region Implementation of IHolidayAPIClient

        /// <summary>
        /// Gets the usage data.
        /// </summary>
        /// <value>
        /// The usage data.
        /// </value>
        public RequestMetadata UsageData => _metadata;

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="year">The year.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<IHoliday>> GetHolidaysAsync(string country, int year, CancellationToken cancellationToken)
        {
            return await GetHolidaysAsync(new HolidayFilter(country, year), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
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
            return response?.Holidays;
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(CancellationToken cancellationToken)
        {
            return await GetCountriesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ICountry>> GetCountriesAsync(string search, CancellationToken cancellationToken)
        {
            var request = new CountriesRequest { Key = _apiKey };
            if (!string.IsNullOrWhiteSpace(search))
                request.Search = search;
            var response = await Execute<CountriesRequest, CountriesResponse>(request, cancellationToken).ConfigureAwait(false);
            return response?.Countries;
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(CancellationToken cancellationToken)
        {
            return await GetLanguagesAsync(string.Empty, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<ILanguage>> GetLanguagesAsync(string search, CancellationToken cancellationToken)
        {
            var request = new LanguagesRequest { Key = _apiKey };
            if (!string.IsNullOrWhiteSpace(search))
                request.Search = search;
            var response = await Execute<LanguagesRequest, LanguagesResponse>(request, cancellationToken).ConfigureAwait(false);
            return response?.Languages;
        }

        #endregion
    }
}
