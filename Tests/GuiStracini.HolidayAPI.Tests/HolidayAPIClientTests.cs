namespace GuiStracini.HolidayAPI.Tests
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// The holiday API client tests class.
    /// </summary>
    public class HolidayAPIClientTests
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HolidayAPIClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayAPIClientTests"/> class.
        /// </summary>
        public HolidayAPIClientTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<HolidayAPIClientTests>()
                .AddEnvironmentVariables()
                .Build();
            var apiKey = string.IsNullOrWhiteSpace(configuration["apiKey"])
                ? "__YOUR_API_KEY_HERE__"
                : configuration["apiKey"];
            _client = new HolidayAPIClient(apiKey);
        }
        /// <summary>
        /// Gets the brazilian holidays from the last year.
        /// </summary>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYear()
        {
            var result = await _client.GetHolidaysAsync("BR", DateTime.Now.AddYears(-1).Year, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);
            Assert.True(result.Any());
        }

        /// <summary>
        /// Gets the countries.
        /// </summary>
        [Fact]
        public async Task GetCountries()
        {
            var result = await _client.GetCountriesAsync(CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);
            Assert.True(result.Any());
        }

        [Fact]
        public async Task GetLanguages()
        {
            var result = await _client.GetLanguagesAsync(CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);
            Assert.True(result.Any());
        }
    }
}
