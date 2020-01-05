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
            var regex = new Regex(@"/?(?<pattern>{(?<propertyName>\w+?)})/?");
            if (!regex.IsMatch(endpoint))
                return endpoint;
            var used = 0;
            var skiped = 0;
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
                    var defaultValue = string.Empty;
                    if (property.GetCustomAttributes(typeof(DefaultRouteValueAttribute), false) is
                            DefaultRouteValueAttribute[] defaultsValues && defaultsValues.Any())
                        defaultValue = defaultsValues.Single().DefaultValue;
                    endpoint = endpoint.Replace(match.Value, defaultValue);
                    if (skiped == 0 && defaultValue == string.Empty)
                        skiped = counter;
                    continue;
                }
                used = counter;
                endpoint = endpoint.Replace(match.Groups["pattern"].Value, propertyValue.ToString());
            }
            if (skiped != 0 && skiped < used)
                throw new InvalidRequestEndpointException(originalEndpoint, endpoint);
            return endpoint.Trim('/');
        }

    }
}
