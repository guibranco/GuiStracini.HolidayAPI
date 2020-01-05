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
        private ServiceFactory _serviceFactory;

        #region ~Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayAPIClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public HolidayAPIClient(string apiKey)
        {
            _apiKey = new Guid(apiKey);
            _metadata = new RequestMetadata
            {
                Message = "Make at least on request before get the metadata"
            };
            _serviceFactory = new ServiceFactory();
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
            var data = new HolidayRequest();

            var result = await _serviceFactory.Get<HolidayRequest, HolidayResponse>(data, cancellationToken).ConfigureAwait(false);

            _metadata = result.Requests;
            _metadata.LastCall = DateTime.Now;

            if (result.Status == 200)
            {
                _metadata.Message = "Success";
                return result.Holidays;
            }

            _metadata.Message = $"Error code: {result.Status}";
            return null;
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
