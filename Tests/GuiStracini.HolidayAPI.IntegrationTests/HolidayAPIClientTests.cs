// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 01-05-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidayAPIClientTests.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.IntegrationTests
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using GuiStracini.HolidayAPI.GoodPractices;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    /// <summary>
    /// The holiday API client tests class.
    /// </summary>
    public class HolidayApiClientTests
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HolidayApiClient _client;

        /// <summary>
        /// Creates the HTTP client.
        /// </summary>
        /// <returns>HttpClient.</returns>
        private static HttpClient CreateHttpClient()
        {
            var httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri("https://holidayapi.com/");
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            return httpClient;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayApiClientTests" /> class.
        /// </summary>
        public HolidayApiClientTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<HolidayApiClientTests>(true)
                .AddEnvironmentVariables()
                .Build();
            var apiKey = string.IsNullOrWhiteSpace(configuration["apiKey"])
                ? "__YOUR_API_KEY_HERE__"
                : configuration["apiKey"];
            _client = new HolidayApiClient(apiKey, CreateHttpClient());
        }

        /// <summary>
        /// Gets the holidays with invalid key.
        /// </summary>
        [Fact]
        public async Task GetHolidaysWithInvalidKey()
        {
            var year = DateTime.Now.Year - 1;
            var client = new HolidayApiClient(Guid.Empty.ToString(), CreateHttpClient());
            var ex = await Assert.ThrowsAsync<HolidayApiException>(
                async () => await client.GetHolidaysAsync("BR", year, CancellationToken.None)
            );
            Assert.Equal(
                "401 - Invalid API key. For more information, please visit https://holidayapi.com/docs",
                ex.Message
            );
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        [Fact]
        public async Task GetMetadata()
        {
            var now = DateTime.Now;
            var year = DateTime.Now.Year - 1;
            await _client.GetHolidaysAsync("BR", year, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.Equal(
                "These results do not include state and province holidays. For more information, please visit https://holidayapi.com/docs",
                metadata.Warning
            );
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
            var year = DateTime.Now.Year - 1;
            var result = await _client.GetHolidaysAsync("BR", year, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.Contains(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 12, 25))
                    && holiday.Name.Equals("Christmas Day")
            );
            Assert.Contains(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 9, 7))
                    && holiday.Name.Equals("Independence Day")
            );
            Assert.DoesNotContain(
                list,
                holiday => holiday.Name.Equals("George Washington's Birthday")
            );
        }

        /// <summary>
        /// Gets the brazilian holidays from the last year with search.
        /// </summary>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYearWithSearch()
        {
            var year = DateTime.Now.Year - 1;
            var filter = new HolidayFilter("BR", year) { Search = "Father" };
            var result = await _client.GetHolidaysAsync(filter, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.DoesNotContain(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 12, 25))
                    && holiday.Name.Equals(
                        "Christmas Day",
                        StringComparison.InvariantCultureIgnoreCase
                    )
            );
            Assert.DoesNotContain(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 9, 7))
                    && holiday.Name.Equals(
                        "Independence Day",
                        StringComparison.InvariantCultureIgnoreCase
                    )
            );
            Assert.Contains(
                list,
                holiday =>
                    holiday.Date.Month.Equals(8)
                    && holiday.Date.DayOfWeek == DayOfWeek.Sunday
                    && holiday.Name.Equals(
                        "Father's Day",
                        StringComparison.InvariantCultureIgnoreCase
                    )
            );
        }

        /// <summary>
        /// Asynchronously retrieves Brazilian holidays from the previous year with specified filters and validates the results.
        /// </summary>
        /// <remarks>
        /// This test method is designed to ensure that the holiday retrieval functionality works correctly for the Brazilian holidays of the previous year.
        /// It sets up a filter to search for holidays in December, specifically focusing on Christmas, while also checking for metadata about the request.
        /// The method asserts that the response is successful and that the usage data indicates that the API was used.
        /// It also verifies that certain holidays, such as "Christmas Day" and "Independence Day," are not present in the results, while ensuring that
        /// a holiday containing "Natal" (the Portuguese word for Christmas) is included in the list.
        /// The test uses various assertions to confirm that the expected conditions are met, ensuring the reliability of the holiday retrieval feature.
        /// </remarks>
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYearWithFilters()
        {
            var year = DateTime.Now.Year - 1;
            var filter = new HolidayFilter("BR", year)
            {
                Month = 12,
                Day = 26,
                Previous = true,
                Upcoming = false,
                Language = "pt",
                Public = true,
                Subdivisions = false,
                Search = "Christmas",
            };
            var result = await _client.GetHolidaysAsync(filter, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            Assert.NotNull(metadata.Warning);

            var list = result.ToList();
            Assert.True(list.Any());
            Assert.DoesNotContain(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 12, 25))
                    && holiday.Name.Equals(
                        "Christmas Day",
                        StringComparison.InvariantCultureIgnoreCase
                    )
            );
            Assert.DoesNotContain(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 9, 7))
                    && holiday.Name.Equals(
                        "Independence Day",
                        StringComparison.InvariantCultureIgnoreCase
                    )
            );
            Assert.Contains(
                list,
                holiday =>
                    holiday.Date.Equals(new DateTime(year, 12, 25))
                    && holiday.Name.Contains("Natal", StringComparison.InvariantCultureIgnoreCase)
            );
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
            Assert.Contains(
                list,
                country => country.Flag.Equals("https://flagsapi.com/BR/flat/64.png")
            );
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

        /// <summary>
        /// Defines the test method GetWorkday.
        /// </summary>
        [Fact]
        public async Task GetWorkday()
        {
            const int days = 10;
            var date = new DateTime(DateTime.Now.Year - 1, 1, 1);

            var result = await _client.GetWorkdayAsync("BR", date, days, CancellationToken.None);
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            Assert.True(result.Date > date.AddDays(days));
        }

        /// <summary>
        /// Defines the test method GetWorkdays.
        /// </summary>
        [Fact]
        public async Task GetWorkdays()
        {
            const int days = 10;
            var date = new DateTime(DateTime.Now.Year - 1, 1, 1);

            var result = await _client.GetWorkdaysAsync(
                "BR",
                date,
                date.AddDays(days),
                CancellationToken.None
            );
            var metadata = _client.UsageData;

            Assert.Equal("Success", metadata.Message);
            Assert.True(metadata.Used > 0);

            Assert.True(result < days);
        }
    }
}
