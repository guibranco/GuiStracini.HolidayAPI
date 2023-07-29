// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="HolidayAPIException.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.GoodPractices
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    /// The HolidayAPI exception class.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class HolidayApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayApiException" /> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="innerException">The inner exception.</param>
        public HolidayApiException(string endpoint, Exception innerException)
            : base($"Unable to complete request to {endpoint}", innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HolidayApiException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="errorMessage">The error message.</param>
        public HolidayApiException(int statusCode, string errorMessage)
            : base($"{statusCode} - {errorMessage}") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"></see> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info">info</paramref> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0).</exception>
        private HolidayApiException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
