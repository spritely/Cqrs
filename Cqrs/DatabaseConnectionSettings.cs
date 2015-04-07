// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseConnectionSettings.cs">
//   Copyright (c) 2015. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs
{
    /// <summary>
    ///     Defines settings for constructing database connection strings.
    /// </summary>
    public class DatabaseConnectionSettings
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DatabaseConnectionSettings" /> is asynchronous.
        /// </summary>
        /// <value>
        ///     <c>true</c> if asynchronous; otherwise, <c>false</c>.
        /// </value>
        public bool Async { get; set; }

        /// <summary>
        ///     Gets or sets the connection timeout in seconds.
        /// </summary>
        /// <value>
        ///     The connection timeout in seconds.
        /// </value>
        public int? ConnectionTimeoutInSeconds { get; set; }

        /// <summary>
        ///     Gets or sets the credentials.
        /// </summary>
        /// <value>
        ///     The credentials.
        /// </value>
        public Credentials Credentials { get; set; }

        /// <summary>
        ///     Gets or sets the name of the database.
        /// </summary>
        /// <value>
        ///     The database name.
        /// </value>
        public string Database { get; set; }

        /// <summary>
        ///     Gets or sets the default command timeout in seconds.
        /// </summary>
        /// <value>
        ///     The default command timeout in seconds.
        /// </value>
        public int? DefaultCommandTimeoutInSeconds { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="DatabaseConnectionSettings" /> is encrypted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if encrypt; otherwise, <c>false</c>.
        /// </value>
        public bool Encrypt { get; set; }

        /// <summary>
        ///     Gets or sets more options which is a string that will be appended to the connection string (an extensability point
        ///     if the supplied options are insufficient).
        /// </summary>
        /// <value>
        ///     The more options.
        /// </value>
        public string MoreOptions { get; set; }

        /// <summary>
        ///     Gets or sets the server name.
        /// </summary>
        /// <value>
        ///     The server name.
        /// </value>
        public string Server { get; set; }
    }
}
