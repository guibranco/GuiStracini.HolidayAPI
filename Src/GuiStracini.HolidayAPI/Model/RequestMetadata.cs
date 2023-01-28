// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="RequestMetadata.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    using System;

    /// <summary>
    /// The request metadata class.
    /// Display metadata information about API consumption. How many calls was used, is still available and when the counter resets for the given API key.
    /// </summary>
    public class RequestMetadata
    {
        /// <summary>
        /// Gets or sets the used.
        /// </summary>
        /// <value>The used.</value>
        public int Used { get; set; }

        /// <summary>
        /// Gets or sets the available.
        /// </summary>
        /// <value>The available.</value>
        public int Available { get; set; }

        /// <summary>
        /// Gets or sets the resets.
        /// </summary>
        /// <value>The resets.</value>
        public DateTime Resets { get; set; }

        /// <summary>
        /// Gets or sets the last call.
        /// </summary>
        /// <value>The last call.</value>
        public DateTime LastCall { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the warning.
        /// </summary>
        /// <value>The warning.</value>
        public string Warning { get; set; }
    }
}
