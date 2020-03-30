using System;

namespace GuiStracini.HolidayAPI.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AdditionalRouteValueAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether the additional parameter should be added as query string to the url.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [as query string]; otherwise, <c>false</c>.
        /// </value>
        public bool AsQueryString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalRouteValueAttribute" /> class.
        /// </summary>
        /// <param name="asQueryString">if set to <c>true</c> the additional parameter is added in the url as query string.</param>
        public AdditionalRouteValueAttribute(bool asQueryString = false)
        {
            AsQueryString = asQueryString;
        }
    }
}
