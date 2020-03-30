namespace GuiStracini.HolidayAPI.Tests
{
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

        /// <summary>
        /// Requests the endpoint with additional values.
        /// </summary>
        [Fact]
        public void RequestEndpointWithAdditionalValues()
        {
            const string expected = "something/test/?foo=bar";
            var dummy = new DummyRequest
            {
                Dummy = "test",
                Foo = "bar"
            };

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void RequestEndpointWithAdditionalValuesNullable()
        {
            const string expected = "something/test/?fooBar=true";
            var dummy = new DummyRequest
            {
                Dummy = "test",
                FooBar = true
            };

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }
    }
}
