namespace GuiStracini.HolidayAPI
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;

    public interface IHolidayAPIClient
    {
        /// <summary>
        /// Gets the usage data.
        /// </summary>
        /// <value>
        /// The usage data.
        /// </value>
        RequestMetadata UsageData { get; }

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="year">The year.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<IHoliday>> GetHolidaysAsync(string country, int year, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the holidays asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<IHoliday>> GetHolidaysAsync(HolidayFilter filter, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<ICountry>> GetCountriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the countries asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<ICountry>> GetCountriesAsync(string search, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<ILanguage>> GetLanguagesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets the languages asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<ILanguage>> GetLanguagesAsync(string search, CancellationToken cancellationToken);
    }
}
