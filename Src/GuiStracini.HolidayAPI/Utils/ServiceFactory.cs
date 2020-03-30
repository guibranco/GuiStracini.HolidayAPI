namespace GuiStracini.HolidayAPI.Utils
{
    using GoodPractices;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Transport;

    public class ServiceFactory
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceFactory"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public ServiceFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Posts the specified data.
        /// </summary>
        /// <typeparam name="TIn">The type of the in.</typeparam>
        /// <typeparam name="TOut">The type of the out.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="HolidayAPIException"></exception>
        public async ValueTask<TOut> Post<TIn, TOut>(TIn data, CancellationToken cancellationToken) where TIn : BaseRequest where TOut : BaseResponse
        {
            var endpoint = data.GetRequestEndpoint();
            try
            {
                var response = await _httpClient.GetAsync(endpoint, cancellationToken).ConfigureAwait(false);
                return await response.Content.ReadAsAsync<TOut>(cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                throw new HolidayAPIException(endpoint, e);
            }
        }

    }
}
