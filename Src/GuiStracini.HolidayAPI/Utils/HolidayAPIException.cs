﻿namespace GuiStracini.HolidayAPI.Utils
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The HolidayAPI exception class.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class HolidayAPIException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayAPIException"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="innerException">The inner exception.</param>
        public HolidayAPIException(string endpoint, Exception innerException)
            : base($"Unable to complete request to {endpoint}", innerException)
        { }

        /// <summary>Initializes a new instance of the <see cref="T:System.Exception"></see> class with serialized data.</summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0).</exception>
        protected HolidayAPIException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
