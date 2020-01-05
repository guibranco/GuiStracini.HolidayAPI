namespace GuiStracini.HolidayAPI.Utils
{
    using System.Linq;
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
            return endpointAttribute == null ? type.Name.ToUpper() : endpointAttribute.EndPoint;
        }

    }
}
