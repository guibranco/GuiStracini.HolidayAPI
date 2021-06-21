// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="RequestHelpers.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Utils
{
    using Newtonsoft.Json;
    using System.Reflection;
    using System.Text;
    using GuiStracini.HolidayAPI.GoodPractices;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using GuiStracini.HolidayAPI.Transport;

    /// <summary>
    /// Class RequestHelpers.
    /// </summary>
    public static class RequestHelpers
    {
        /// <summary>
        /// Gets the request endpoint attribute.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>EndpointRouteAttribute.</returns>
        private static EndpointRouteAttribute GetRequestEndpointAttribute(this BaseRequest request)
        {
            if (!(request.GetType().GetCustomAttributes(typeof(EndpointRouteAttribute), false) is EndpointRouteAttribute
                    []
                    endpoints) ||
                !endpoints.Any())
            {
                return null;
            }

            return endpoints.Single();
        }

        /// <summary>
        /// Gets the request endpoint.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>String.</returns>
        /// <exception cref="GuiStracini.HolidayAPI.GoodPractices.InvalidRequestEndpointException"></exception>
        public static string GetRequestEndpoint(this BaseRequest request)
        {
            var type = request.GetType();
            var endpointAttribute = request.GetRequestEndpointAttribute();
            if (endpointAttribute == null)
            {
                return type.Name.ToUpper();
            }

            var originalEndpoint = endpointAttribute.EndPoint;
            var endpoint = originalEndpoint;
            var additional = request.GetRequestAdditionalRouteValue();
            var finalSlash = endpoint.EndsWith("/") ? string.Empty : "/";
            if (!string.IsNullOrWhiteSpace(additional))
            {
                endpoint += $"{(endpoint.Contains("?") ? "&" : $"{finalSlash}?")}{additional}";
            }

            var regex = new Regex(@"/?(?<pattern>{(?<propertyName>\w+?)})/?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase, new TimeSpan(0, 0, 30));
            if (!regex.IsMatch(endpoint))
            {
                return endpoint;
            }

            var used = 0;
            var skipped = 0;
            var counter = 0;
            foreach (Match match in regex.Matches(endpoint))
            {
                ProcessMatch(request, match, type, originalEndpoint, ref counter, ref endpoint, ref skipped, ref used);
            }

            if (skipped != 0 && skipped < used)
            {
                throw new InvalidRequestEndpointException(originalEndpoint, endpoint);
            }

            return endpoint.Trim('/');
        }

        /// <summary>
        /// Processes the match.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="match">The match.</param>
        /// <param name="type">The type.</param>
        /// <param name="originalEndpoint">The original endpoint.</param>
        /// <param name="counter">The counter.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="skipped">The skipped.</param>
        /// <param name="used">The used.</param>
        /// <exception cref="GuiStracini.HolidayAPI.GoodPractices.EndpointRouteBadFormatException"></exception>
        /// <exception cref="EndpointRouteBadFormatException"></exception>
        private static void ProcessMatch(
            BaseRequest request,
            Match match,
            Type type,
            string originalEndpoint,
            ref int counter,
            ref string endpoint,
            ref int skipped,
            ref int used)
        {
            counter++;
            var propertyName = match.Groups["propertyName"].Value;
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                throw new EndpointRouteBadFormatException(originalEndpoint);
            }

            var propertyType = property.PropertyType;
            var propertyValue = property.GetValue(request, null);
            if (propertyValue == null ||
                propertyType == typeof(int) && Convert.ToInt32(propertyValue) == 0 ||
                propertyType == typeof(long) && Convert.ToInt64(propertyValue) == 0 ||
                propertyType == typeof(decimal) && Convert.ToDecimal(propertyValue) == new decimal(0) ||
                propertyType == typeof(string) && string.IsNullOrEmpty(propertyValue.ToString()))
            {
                endpoint = endpoint.Replace(match.Value, string.Empty);
                if (skipped == 0)
                {
                    skipped = counter;
                }

                return;
            }
            used = counter;
            endpoint = endpoint.Replace(match.Groups["pattern"].Value, propertyValue.ToString());
        }

        /// <summary>
        /// Gets the request additional route value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        private static string GetRequestAdditionalRouteValue(this BaseRequest request)
        {
            var type = request.GetType();
            var properties = type.GetProperties().Where(prop => prop.IsDefined(typeof(AdditionalRouteValueAttribute), false)).ToList();
            if (!properties.Any())
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            var addAsQueryString = false;
            foreach (var property in properties)
            {
                if (!(property.GetCustomAttributes(typeof(AdditionalRouteValueAttribute), false)
                    is AdditionalRouteValueAttribute[] attributes) || !attributes.Any())
                {
                    continue;
                }

                addAsQueryString = attributes.Single().AsQueryString;

                var propertyValue = property.GetValue(request);
                if (propertyValue == null)
                {
                    continue;
                }

                var propertyType = GetPropertyType(property);

                if (propertyType == typeof(bool))
                {
                    propertyValue = propertyValue.ToString().ToLower();
                }

                var propertyName = GetPropertyName(property);

                if (propertyType == typeof(string) ||
                    propertyType == typeof(bool) ||
                    propertyType == typeof(int) && Convert.ToInt32(propertyValue) > 0 ||
                    propertyType == typeof(long) && Convert.ToInt64(propertyValue) > 0)
                {
                    builder.AppendFormat("{0}", addAsQueryString ? $"{propertyName}=" : string.Empty)
                        .Append(propertyValue).Append(addAsQueryString ? "&" : "/");
                }
            }

            var result = builder.ToString();
            if (addAsQueryString && result.Length > 1)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>System.String.</returns>
        private static string GetPropertyName(PropertyInfo property)
        {
            var propertyName = property.Name;
            if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false) is JsonPropertyAttribute[]
                    attributesJson && attributesJson.Any())
            {
                propertyName = attributesJson.Single().PropertyName;
            }

            return propertyName;
        }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Type.</returns>
        private static Type GetPropertyType(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            if (Nullable.GetUnderlyingType(propertyType) != null)
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
            }

            return propertyType;
        }
    }
}
