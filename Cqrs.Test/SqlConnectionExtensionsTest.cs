// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlConnectionExtensionsTest.cs">
//   Copyright (c) 2015. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs.Test
{
    using System.Security;
    using NUnit.Framework;

    [TestFixture]
    public class SqlConnectionExtensionsTest
    {
        [Test]
        public void CreateSqlConnection_defaults_Async_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Async=False"));
        }

        [Test]
        public void CreateSqlConnection_sets_Async()
        {
            var settings = new DatabaseConnectionSettings
            {
                Async = true
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Async=True"));
        }

        [Test]
        public void CreateSqlConnection_defaults_Encrypt_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Encrypt=False"));
        }

        [Test]
        public void CreateSqlConnection_sets_Encrypt()
        {
            var settings = new DatabaseConnectionSettings
            {
                Encrypt = true
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Encrypt=True"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_Server_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Server"));
        }

        [Test]
        public void CreateSqlConnection_sets_Server()
        {
            var settings = new DatabaseConnectionSettings
            {
                Server = "Test"
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Server=Test"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_Database_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Database"));
        }

        [Test]
        public void CreateSqlConnection_sets_Database()
        {
            var settings = new DatabaseConnectionSettings
            {
                Database = "Test"
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Database=Test"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_ConnectionTimeout_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Connection Timeout"));
        }

        [Test]
        public void CreateSqlConnection_sets_ConnectionTimeout()
        {
            var settings = new DatabaseConnectionSettings
            {
                ConnectionTimeoutInSeconds = 30
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Connection Timeout=30"));
        }

        [Test]
        public void CreateSqlConnection_sets_Integrated_Security_if_no_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Integrated Security=True"));
        }

        [Test]
        public void CreateSqlConnection_does_not_set_Integrated_Security_if_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings()
            {
                Credentials = new Credentials
                {
                    User = string.Empty,
                    Password = new SecureString()
                }
            };
            settings.Credentials.Password.MakeReadOnly();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Integrated Security"));
        }

        [Test]
        public void CreateSqlConnection_sets_SqlCredentials()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    User = "User",
                    Password = new SecureString()
                }
            };
            "Password".ToCharArray().ForEach(c => settings.Credentials.Password.AppendChar(c));

            settings.Credentials.Password.MakeReadOnly();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.Credential.UserId, Is.EqualTo(settings.Credentials.User));
            Assert.That(sqlConnection.Credential.Password, Is.EqualTo(settings.Credentials.Password));
        }
    }
}
