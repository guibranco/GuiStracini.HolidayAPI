// ***********************************************************************
// Assembly         : GuiStracini.HolidayAPI
// Author           : Guilherme Branco Stracini
// Created          : 06-23-2020
//
// Last Modified By : Guilherme Branco Stracini
// Last Modified On : 01-05-2022
// ***********************************************************************
// <copyright file="BaseResponse.cs" company="Guilherme Branco Stracini">
//     © 2020 Guilherme Branco Stracini. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace GuiStracini.HolidayAPI.Transport
{
    using Model;

    /// <summary>
    /// The base response class.
    /// </summary>
    /// <seealso cref="GuiStracini.HolidayAPI.Model.IResponseMetadata" />
    public class BaseResponse : IResponseMetadata
    {
        #region Implementation of IResponseMetadata

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the warning.
        /// </summary>
        /// <value>The warning.</value>
        public string Warning { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the requests.
        /// </summary>
        /// <value>The requests.</value>
        public RequestMetadata Requests { get; set; }

        #endregion
    }
}
