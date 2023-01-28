// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 03-02-2022
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="ProcessMatchData.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Utils
{
    using System;

    /// <summary>
    /// Class ProcessMatchData.
    /// </summary>
    public class ProcessMatchData
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>The endpoint.</value>
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the original endpoint.
        /// </summary>
        /// <value>The original endpoint.</value>
        public string OriginalEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the used.
        /// </summary>
        /// <value>The used.</value>
        public int Used { get; set; }
        /// <summary>
        /// Gets or sets the skipped.
        /// </summary>
        /// <value>The skipped.</value>
        public int Skipped { get; set; }
        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        /// <value>The counter.</value>
        public int Counter { get; set; }
    }
}
