// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 06-23-2020
// ***********************************************************************
// <copyright file="EndpointRouteBadFormatException.cs" company="Guilherme Branco Stracini">
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
    /// Throws when a request endpoint is in a bad format
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EndpointRouteBadFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointRouteBadFormatException" /> class.
        /// </summary>
        /// <param name="endpointFormat">The endpoint format.</param>
        public EndpointRouteBadFormatException(string endpointFormat)
            : base($"Unable to resolve the endpoint format {endpointFormat}")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        private EndpointRouteBadFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
