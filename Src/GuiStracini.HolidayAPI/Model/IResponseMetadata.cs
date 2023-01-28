// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 28/01/2023
// ***********************************************************************
// <copyright file="IResponseMetadata.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Model
{
    /// <summary>
    /// The response metadata interface.
    /// </summary>
    public interface IResponseMetadata
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        int Status { get; set; }

        /// <summary>
        /// Gets or sets the warning.
        /// </summary>
        /// <value>The warning.</value>
        string Warning { get; set; }

        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>The requests.</value>
        RequestMetadata Requests { get; set; }
    }
}
