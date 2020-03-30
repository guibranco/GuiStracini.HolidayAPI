using Newtonsoft.Json;
using System.Text;

namespace GuiStracini.HolidayAPI.Utils
{
    using GoodPractices;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Transport;

    public static class RequestHelpers
    {
        /// <summary>
        /// Gets the request endpoint attribute.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private static EndpointRouteAttribute GetRequestEndpointAttribute(this BaseRequest request)
        {
            if (!(request.GetType().GetCustomAttributes(typeof(EndpointRouteAttribute), false) is EndpointRouteAttribute[]
                    endpoints) ||
                !endpoints.Any())
                return null;
            return endpoints.Single();
        }

        /// <summary>
        /// Gets the request endpoint.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>String.</returns>
        public static string GetRequestEndpoint(this BaseRequest request)
        {
            var type = request.GetType();
            var endpointAttribute = request.GetRequestEndpointAttribute();
            if (endpointAttribute == null)
                return type.Name.ToUpper();
            var originalEndpoint = endpointAttribute.EndPoint;
            var endpoint = originalEndpoint;
            var additional = request.GetRequestAdditionalRouteValue();
            if (!string.IsNullOrWhiteSpace(additional))
                endpoint += $"{(endpoint.Contains("?") ? "&" : (endpoint.EndsWith("/") ? string.Empty : "/") + "?")}{additional}";
            var regex = new Regex(@"/?(?<pattern>{(?<propertyName>\w+?)})/?");
            if (!regex.IsMatch(endpoint))
                return endpoint;
            var used = 0;
            var skipped = 0;
            var counter = 0;
            foreach (Match match in regex.Matches(endpoint))
            {
                counter++;
                var propertyName = match.Groups["propertyName"].Value;
                var property = type.GetProperty(propertyName);
                if (property == null)
                    throw new RequestEndpointBadFormatException(originalEndpoint);
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
                        skipped = counter;
                    continue;
                }
                used = counter;
                endpoint = endpoint.Replace(match.Groups["pattern"].Value, propertyValue.ToString());
            }
            if (skipped != 0 && skipped < used)
                throw new InvalidRequestEndpointException(originalEndpoint, endpoint);
            return endpoint.Trim('/');
        }

        /// <summary>
        /// Gets the request additional route value.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private static string GetRequestAdditionalRouteValue(this BaseRequest request)
        {
            var type = request.GetType();
            var properties = type.GetProperties().Where(prop => prop.IsDefined(typeof(AdditionalRouteValueAttribute), false)).ToList();
            if (!properties.Any())
                return string.Empty;
            var builder = new StringBuilder();
            var addAsQueryString = false;
            foreach (var property in properties)
            {
                if (!(property.GetCustomAttributes(typeof(AdditionalRouteValueAttribute), false) is AdditionalRouteValueAttribute[] attributes) || !attributes.Any())
                    continue;
                addAsQueryString = attributes.Single().AsQueryString;
                var propertyValue = property.GetValue(request);
                if (propertyValue == null)
                    continue;

                var propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(propertyType) != null)
                    propertyType = Nullable.GetUnderlyingType(propertyType);

                if (propertyType == null)
                    continue;

                if (propertyType == typeof(bool))
                    propertyValue = propertyValue.ToString().ToLower();

                var propertyName = property.Name;
                if (property.GetCustomAttributes(typeof(JsonPropertyAttribute), false) is JsonPropertyAttribute[] attributesJson &&
                    attributesJson.Any())
                    propertyName = attributesJson.Single().PropertyName;

                if (propertyType == typeof(string) ||
                    propertyType == typeof(bool) ||
                    propertyType == typeof(int) && Convert.ToInt32(propertyValue) > 0 ||
                    propertyType == typeof(long) && Convert.ToInt64(propertyValue) > 0)
                    builder.AppendFormat("{0}", addAsQueryString ? $"{propertyName}=" : string.Empty).Append(propertyValue).Append(addAsQueryString ? "&" : "/");
            }

            var result = builder.ToString();
            if (addAsQueryString && result.Length > 1)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

    }
}
