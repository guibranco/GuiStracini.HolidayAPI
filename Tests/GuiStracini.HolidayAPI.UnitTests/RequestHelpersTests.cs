// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI.Tests
// Author           : Guilherme Branco Stracini
// Created          : 01-05-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 03-02-2022
// ***********************************************************************
// <copyright file="RequestHelpersTests.cs" company="GuiStracini.HolidayAPI.Tests">
//     Copyright (c) Guilherme Branco Stracini ME. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace GuiStracini.HolidayAPI.UnitTests
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using GuiStracini.HolidayAPI.GoodPractices;
    using GuiStracini.HolidayAPI.UnitTests.Requests;
    using GuiStracini.HolidayAPI.Utils;
    using Transport;
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
            var dummy = new DummyRequest { Dummy = "my-string" };

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
            var dummy = new DummyRequest { Dummy = "test", Foo = "bar" };

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Defines the test method RequestEndpointWithAdditionalValuesNullable.
        /// </summary>
        [Fact]
        public void RequestEndpointWithAdditionalValuesNullable()
        {
            const string expected = "something/test/?fooBar=true";
            var dummy = new DummyRequest { Dummy = "test", FooBar = true };

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Defines the test method RequestWithoutValidEndpointAttribute.
        /// </summary>
        [Fact]
        public void RequestWithoutValidEndpointAttribute()
        {
            var dummy = new DummyRequestNoEndpointAttribute();

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(
                nameof(DummyRequestNoEndpointAttribute).ToUpper(CultureInfo.InvariantCulture),
                actual
            );
        }

        /// <summary>
        /// Defines the test method RequestEndpointEndingWithSlash.
        /// </summary>
        [Fact]
        public void RequestEndpointEndingWithSlash()
        {
            const string expected = "something/test";
            var dummy = new DummyRequestEndSlash { Dummy = "test" };

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Defines the test method RequestEndpointNoVariables.
        /// </summary>
        [Fact]
        public void RequestEndpointNoVariables()
        {
            const string expected = "something";
            var dummy = new DummyRequestNoVariable();

            var actual = dummy.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Defines the test method RequestEndpointInvalidProperty.
        /// </summary>
        [Fact]
        public async Task RequestEndpointInvalidProperty()
        {
            const string expected = "Unable to resolve the endpoint format something/{Invalid}";

            var dummy = new DummyRequestInvalidProperty();

            var exception = await Assert.ThrowsAsync<EndpointRouteBadFormatException>(
                () => Task.FromResult(dummy.GetRequestEndpoint())
            );

            Assert.Equal(expected, exception.Message);
        }

        /// <summary>
        /// Tests the generation of the request endpoint for holiday data with additional values.
        /// </summary>
        /// <remarks>
        /// This unit test verifies that the <see cref="HolidayRequest"/> class correctly constructs a request endpoint
        /// for retrieving holiday information based on the specified parameters. The test checks that the generated
        /// endpoint matches the expected URL format, which includes the API key, country code, year, and subdivision
        /// information. It ensures that the method <see cref="HolidayRequest.GetRequestEndpoint"/> behaves as expected
        /// when provided with a set of predefined values.
        /// </remarks>
        [Fact]
        public void RequestEndpointWithAdditionalValuesHolidays()
        {
            const string expected =
                "v1/holidays?key=00000000-0000-0000-0000-000000000000&country=XX&year=2024&subdivisions=true";
            var holidaysRequest = new HolidayRequest
            {
                Key = Guid.Empty,
                Country = "XX",
                Year = 2024,
                SubDivisions = true,
            };

            var actual = holidaysRequest.GetRequestEndpoint();

            Assert.Equal(expected, actual);
        }
    }
}
