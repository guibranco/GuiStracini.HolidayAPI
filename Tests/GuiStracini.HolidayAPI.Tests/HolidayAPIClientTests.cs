namespace GuiStracini.HolidayAPI.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class HolidayAPIClientTests
    {
        [Fact]
        public async Task GetBrazilianHolidaysFromTheLastYear()
        {
            var client = new HolidayAPIClient("");
            var result = await client.GetHolidaysAsync("BR", DateTime.Now.AddYears(-1).Year, CancellationToken.None);
            var metadata = client.UsageData;

            Assert.True(metadata.Used > 0);
            Assert.True(result.Any());
        }
    }
}
