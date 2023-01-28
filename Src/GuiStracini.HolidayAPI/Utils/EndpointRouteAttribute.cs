// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="EndpointRouteAttribute.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Utils
{
    using System;

    /// <summary>
    /// Class EndpointRouteAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class EndpointRouteAttribute : Attribute
    {
        #region ~Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointRouteAttribute" /> class.
        /// </summary>
        /// <param name="endPoint">The end point path of the request.</param>
        public EndpointRouteAttribute(string endPoint)
        {
            EndPoint = endPoint;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>The end point path.</value>
        public string EndPoint { get; }

        #endregion

    }
}
