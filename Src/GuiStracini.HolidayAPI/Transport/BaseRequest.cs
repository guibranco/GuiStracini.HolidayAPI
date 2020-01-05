namespace GuiStracini.HolidayAPI.Transport
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// The base request class.
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [JsonProperty("key")]
        public Guid Key { get; set; }
    }
}
