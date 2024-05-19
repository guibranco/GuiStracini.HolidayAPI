// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="RequestHelpers.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Utils
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using GuiStracini.HolidayAPI.GoodPractices;
    using GuiStracini.HolidayAPI.Transport;
    using Newtonsoft.Json;

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
            if (
                !(
                    request.GetType().GetCustomAttributes(typeof(EndpointRouteAttribute), false)
                    is EndpointRouteAttribute[] endpoints
                ) || !endpoints.Any()
            )
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
            var matchData = new ProcessMatchData { Type = request.GetType() };

            var endpointAttribute = request.GetRequestEndpointAttribute();
            if (endpointAttribute == null)
            {
                return matchData.Type.Name.ToUpper();
            }

            matchData.OriginalEndpoint = endpointAttribute.EndPoint;
            matchData.Endpoint = matchData.OriginalEndpoint;

            var additional = request.GetRequestAdditionalRouteValue();
            var finalSlash = matchData.Endpoint.EndsWith("/") ? string.Empty : "/";
            if (!string.IsNullOrWhiteSpace(additional))
            {
                matchData.Endpoint +=
                    $"{(matchData.Endpoint.Contains("?") ? "&" : $"{finalSlash}?")}{additional}";
            }

            var regex = new Regex(
                @"/?(?<pattern>{(?<propertyName>\w+?)})/?",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
                new TimeSpan(0, 0, 30)
            );
            if (!regex.IsMatch(matchData.Endpoint))
            {
                return matchData.Endpoint;
            }

            foreach (Match match in regex.Matches(matchData.Endpoint))
            {
                ProcessMatch(request, match, matchData);
            }

            if (matchData.Skipped != 0 && matchData.Skipped < matchData.Used)
            {
                throw new InvalidRequestEndpointException(
                    matchData.OriginalEndpoint,
                    matchData.Endpoint
                );
            }

            return matchData.Endpoint.Trim('/');
        }

        /// <summary>
        /// Processes the match.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="match">The match.</param>
        /// <param name="matchData">The match data.</param>
        /// <exception cref="GuiStracini.HolidayAPI.GoodPractices.EndpointRouteBadFormatException"></exception>
        private static void ProcessMatch(
            BaseRequest request,
            Match match,
            ProcessMatchData matchData
        )
        {
            matchData.Counter++;
            var propertyName = match.Groups["propertyName"].Value;
            var property = matchData.Type.GetProperty(propertyName);
            if (property == null)
            {
                throw new EndpointRouteBadFormatException(matchData.OriginalEndpoint);
            }

            var propertyType = property.PropertyType;
            var propertyValue = property.GetValue(request, null);
            if (
                propertyValue == null
                || (propertyType == typeof(int) && Convert.ToInt32(propertyValue) == 0)
                || (propertyType == typeof(long) && Convert.ToInt64(propertyValue) == 0)
                || (propertyType == typeof(decimal)
                    && Convert.ToDecimal(propertyValue) == new decimal(0))
                || (propertyType == typeof(string) && string.IsNullOrEmpty(propertyValue.ToString()))
            )
            {
                matchData.Endpoint = matchData.Endpoint.Replace(match.Value, string.Empty);
                if (matchData.Skipped == 0)
                {
                    matchData.Skipped = matchData.Counter;
                }

                return;
            }
            matchData.Used = matchData.Counter;
            matchData.Endpoint = matchData.Endpoint.Replace(
                match.Groups["pattern"].Value,
                propertyValue.ToString()
            );
        }

        /// <summary>
        /// Gets the request additional route value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>System.String.</returns>
        private static string GetRequestAdditionalRouteValue(this BaseRequest request)
        {
            var type = request.GetType();
            var properties = type.GetProperties()
                .Where(prop => prop.IsDefined(typeof(AdditionalRouteValueAttribute), false))
                .ToList();
            if (!properties.Any())
            {
                return string.Empty;
            }

            var builder = new StringBuilder();
            var addAsQueryString = properties.Aggregate(
                false,
                (current, property) => ParseProperty(request, property, current, builder)
            );
            var result = builder.ToString();

            if (addAsQueryString && result.Length > 1)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }

        /// <summary>
        /// Parses the property.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="property">The property.</param>
        /// <param name="addAsQueryString">if set to <c>true</c> [add as query string].</param>
        /// <param name="builder">The builder.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool ParseProperty(
            BaseRequest request,
            PropertyInfo property,
            bool addAsQueryString,
            StringBuilder builder
        )
        {
            if (
                !(
                    property.GetCustomAttributes(typeof(AdditionalRouteValueAttribute), false)
                    is AdditionalRouteValueAttribute[] attributes
                ) || !attributes.Any()
            )
            {
                return addAsQueryString;
            }

            addAsQueryString = attributes.Single().AsQueryString;

            var propertyValue = property.GetValue(request);
            if (propertyValue == null)
            {
                return addAsQueryString;
            }

            var propertyType = GetPropertyType(property);

            if (propertyType == typeof(bool))
            {
                propertyValue = propertyValue.ToString().ToLower(CultureInfo.InvariantCulture);
            }

            var propertyName = GetPropertyName(property);

            if (
                propertyType == typeof(string)
                || propertyType == typeof(bool)
                || (propertyType == typeof(int) && Convert.ToInt32(propertyValue) > 0)
                || (propertyType == typeof(long) && Convert.ToInt64(propertyValue) > 0)
            )
            {
                builder
                    .AppendFormat("{0}", addAsQueryString ? $"{propertyName}=" : string.Empty)
                    .Append(propertyValue)
                    .Append(addAsQueryString ? "&" : "/");
            }

            return addAsQueryString;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>System.String.</returns>
        private static string GetPropertyName(PropertyInfo property)
        {
            var propertyName = property.Name;
            if (
                property.GetCustomAttributes(typeof(JsonPropertyAttribute), false)
                    is JsonPropertyAttribute[] attributesJson
                && attributesJson.Any()
            )
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
