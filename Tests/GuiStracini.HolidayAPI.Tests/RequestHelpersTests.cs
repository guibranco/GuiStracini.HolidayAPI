namespace GuiStracini.HolidayAPI.Tests
{
    using Transport;
    using Utils;
    using Xunit;

    /// <summary>
    /// The request helpers test class
    /// </summary>
    public class RequestHelpersTests
    {
        /// <summary>
        /// Validates the request endpoint.
        /// </summary>
        [Fact]
        public void RequestEndpoint()
        {
            const string expected = "something/my-string";
            var dummy = new DummyRequest
            {
                Dummy = "my-string"
            };
            var actual = dummy.GetRequestEndpoint();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Validates the request endpoint with null values.
        /// </summary>
        [Fact]
        public void RequestEndpointWithNullValues()
        {
            const string expected = "something";
            var dummy = new DummyRequest();
            var actual = dummy.GetRequestEndpoint();
            Assert.Equal(expected, actual);
        }
    }

    [EndpointRoute("something/{Dummy}")]
    public class DummyRequest : BaseRequest
    {
        public string Dummy { get; set; }
    }

}
