namespace GuiStracini.HolidayAPI.Utils
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Transport;

    public class ServiceFactory
    {
        #region Private fields

        /// <summary>
        /// The base endpoint
        /// </summary>
        private const string BaseEndpoint = "https://holidayapi.com/";

        #endregion

        public async ValueTask<TOut> Get<TIn, TOut>(TIn data, CancellationToken cancellationToken) where TIn : BaseRequest where TOut : BaseResponse
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseEndpoint);
                client.DefaultRequestHeaders.ExpectContinue = false;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = await client.GetAsync(data.GetRequestEndpoint(), cancellationToken).ConfigureAwait(false);
                    return await response.Content.ReadAsAsync<TOut>(cancellationToken).ConfigureAwait(false);
                }
                catch (HttpRequestException e)
                {
                    throw new HolidayAPIException(data.GetRequestEndpoint(), e);
                }
            }
        }

    }
}
