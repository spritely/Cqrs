// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlConnectionExtensions.cs">
//   Copyright (c) 2015. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs
{
    using System;
    using System.Data.SqlClient;
    using System.Text;

    public static class SqlConnectionExtensions
    {
        /// <summary>
        ///     Creates a SQLConnection instance from database connection settings. Caller should threat
        ///     this method like a call to new SqlConnection() by disposing of the instance appropriately.
        /// </summary>
        /// <param name="settings">The database connection settings.</param>
        /// <returns>A SQLConnection instance configured with the given database connection settings.</returns>
        public static SqlConnection CreateSqlConnection(this DatabaseConnectionSettings settings)
        {
            if (settings == null)
            {
                throw new NullReferenceException();
            }

            var connectionString = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(settings.Server))
            {
                connectionString.AppendFormat("Server={0};", settings.Server);
            }
            if (!string.IsNullOrWhiteSpace(settings.Database))
            {
                connectionString.AppendFormat("Database={0};", settings.Database);
            }
            connectionString.AppendFormat("Async={0};", settings.Async);
            connectionString.AppendFormat("Encrypt={0};", settings.Encrypt);
            if (settings.ConnectionTimeoutInSeconds.HasValue)
            {
                connectionString.AppendFormat("Connection Timeout={0};", settings.ConnectionTimeoutInSeconds.Value);
            }

            if (settings.Credentials != null)
            {
                if (!string.IsNullOrWhiteSpace(settings.MoreOptions))
                {
                    connectionString.Append(settings.MoreOptions);
                }

                var credentials = new SqlCredential(settings.Credentials.User, settings.Credentials.Password);

                return new SqlConnection(connectionString.ToString(), credentials);
            }

            connectionString.Append("Integrated Security=True;");
            if (!string.IsNullOrWhiteSpace(settings.MoreOptions))
            {
                connectionString.Append(settings.MoreOptions);
            }

            return new SqlConnection(connectionString.ToString());
        }
    }
}
