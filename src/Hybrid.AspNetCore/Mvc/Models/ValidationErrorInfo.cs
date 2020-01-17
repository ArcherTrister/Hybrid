// -----------------------------------------------------------------------
//  <copyright file="ValidationErrorInfo.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2018-08-02 17:56</last-date>
// -----------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace
namespace Hybrid.AspNetCore.Mvc.Models
{
    /// <summary>
    /// Used to store information about a validation error.
    /// </summary>
    [Serializable]
    public class ValidationErrorInfo
    {
        /// <summary>
        /// Validation error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Relate invalid members (fields/properties).
        /// </summary>
        public string[] Members { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        public ValidationErrorInfo()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        public ValidationErrorInfo(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        /// <param name="members">Related invalid members</param>
        public ValidationErrorInfo(string message, string[] members)
            : this(message)
        {
            Members = members;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ValidationErrorInfo"/>.
        /// </summary>
        /// <param name="message">Validation error message</param>
        /// <param name="member">Related invalid member</param>
        public ValidationErrorInfo(string message, string member)
            : this(message, new[] { member })
        {
        }
    }
}