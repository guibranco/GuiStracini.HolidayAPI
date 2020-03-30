using GuiStracini.HolidayAPI.GoodPractices;
using System.Net.Http;
using System.Net.Http.Headers;

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
        /// Creates the HTTP client.
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateHttpClient()
        {
            var httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri("https://holidayapi.com/");
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

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
            _client = new HolidayAPIClient(apiKey, CreateHttpClient());
        }

        /// <summary>
        /// Gets the holidays with invalid key.
        /// </summary>
        [Fact]
        public async Task GetHolidaysWithInvalidKey()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var client = new HolidayAPIClient(Guid.Empty.ToString(), CreateHttpClient());
            var ex = await Assert.ThrowsAsync<HolidayAPIException>(async () => await client.GetHolidaysAsync("BR", year, CancellationToken.None));
            Assert.Equal("401 - Invalid API key. For more information, please visit https://holidayapi.com/docs", ex.Message);
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        [Fact]
        public async Task GetMetadata()
        {
            var now = DateTime.Now;
            var year = DateTime.Now.AddYears(-1).Year;
            await _client.GetHolidaysAsync("BR", year, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.Equal("These results do not include state and province holidays. Please see documentation for more information.", metadata.Warning);
            Assert.True(metadata.Available > 1);
            Assert.True(metadata.Used > 1);
            Assert.True(metadata.LastCall > now);
            Assert.True(metadata.LastCall < DateTime.Now);
            Assert.True(metadata.Resets > DateTime.Now);
        }

        /// <summary>
        /// Gets the brazilian holidays from the last year.
        /// </summary>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYear()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var result = await _client.GetHolidaysAsync("BR", year, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(list, holiday => holiday.Date.Equals(new DateTime(year, 12, 25)) && holiday.Name.Equals("Christmas Day"));
            Assert.Contains(list, holiday => holiday.Date.Equals(new DateTime(year, 9, 7)) && holiday.Name.Equals("Independence Day"));
            Assert.DoesNotContain(list, holiday => holiday.Name.Equals("George Washington's Birthday"));
        }

        /// <summary>
        /// Gets the brazilian holidays from the last year with search.
        /// </summary>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYearWithSearch()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var filter = new HolidayFilter("BR", year)
            {
                Search = "Father"
            };
            var result = await _client.GetHolidaysAsync(filter, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.DoesNotContain(list, holiday => holiday.Date.Equals(new DateTime(year, 12, 25)) && holiday.Name.Equals("Christmas Day"));
            Assert.DoesNotContain(list, holiday => holiday.Date.Equals(new DateTime(year, 9, 7)) && holiday.Name.Equals("Independence Day"));
            Assert.Contains(list, holiday => holiday.Date.Equals(new DateTime(year, 8, 11)) && holiday.Name.Equals("Father's Day"));
        }

        /// <summary>
        /// Gets the brazilian holidays from the last year with filters.
        /// </summary>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYearWithFilters()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var filter = new HolidayFilter("BR", year)
            {
                Month = 12,
                Day = 26,
                Previous = true,
                Upcoming = false,
                Language = "pt",
                Public = true,
                Subdivisions = false,
                Search = "Christmas"
            };
            var result = await _client.GetHolidaysAsync(filter, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            Assert.NotNull(metadata.Warning);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.DoesNotContain(list, holiday => holiday.Date.Equals(new DateTime(year, 12, 25)) && holiday.Name.Equals("Christmas Day"));
            Assert.DoesNotContain(list, holiday => holiday.Date.Equals(new DateTime(year, 9, 7)) && holiday.Name.Equals("Independence Day"));
            Assert.Contains(list, holiday => holiday.Date.Equals(new DateTime(year, 12, 25)) && holiday.Name.Equals("dia de Natal"));
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

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(list, country => country.Name.Equals("Brazil"));
            Assert.Contains(list, country => country.Name.Equals("Argentina"));

            Assert.Contains(list, country => country.Code.Equals("BR"));
            Assert.Contains(list, country => country.Flag.Equals("https://www.countryflags.io/BR/flat/64.png"));
        }

        /// <summary>
        /// Gets the countries with search.
        /// </summary>
        [Fact]
        public async Task GetCountriesWithSearch()
        {
            var result = await _client.GetCountriesAsync("brazil", CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(list, country => country.Name.Equals("Brazil"));
            Assert.DoesNotContain(list, country => country.Name.Equals("Argentina"));
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        [Fact]
        public async Task GetLanguages()
        {
            var result = await _client.GetLanguagesAsync(CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(list, language => language.Code.Equals("pt"));
            Assert.Contains(list, language => language.Code.Equals("es"));
        }

        /// <summary>
        /// Gets the languages with search.
        /// </summary>
        [Fact]
        public async Task GetLanguagesWithSearch()
        {
            var result = await _client.GetLanguagesAsync("portuguese", CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(list, language => language.Code.Equals("pt"));
            Assert.DoesNotContain(list, language => language.Code.Equals("es"));
        }
    }
}
