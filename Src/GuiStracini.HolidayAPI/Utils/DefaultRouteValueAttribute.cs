namespace GuiStracini.HolidayAPI.Utils
{
    using System;

    /// <summary>
    /// The request parameter default value attribute.
    /// This attribute is used when a parameter value is not supplied in the object, instead the default value is used to build de endpoint.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultRouteValueAttribute : Attribute
    {
        #region ~Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRouteValueAttribute"/> class.
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        public DefaultRouteValueAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public string DefaultValue { get; }

        #endregion
    }
}
