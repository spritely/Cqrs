// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlConnectionExtensionsTest.cs">
//   Copyright (c) 2015. All rights reserved.
//   Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Spritely.Cqrs.Test
{
    using NUnit.Framework;
    using Spritely.Recipes;

    [TestFixture]
    public class SqlConnectionExtensionsTest
    {
        [Test]
        public void ToInsecureConnectionString_defaults_Async_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Async=False"));
        }

        [Test]
        public void CreateSqlConnection_defaults_Async_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Async=False"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_Async()
        {
            var settings = new DatabaseConnectionSettings
            {
                Async = true
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Async=True"));
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
        public void ToInsecureConnectionString_defaults_Encrypt_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Encrypt=False"));
        }

        [Test]
        public void CreateSqlConnection_defaults_Encrypt_to_false()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Encrypt=False"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_Encrypt()
        {
            var settings = new DatabaseConnectionSettings
            {
                Encrypt = true
            };
            ;

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Encrypt=True"));
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
        public void ToInsecureConnectionString_does_not_include_Server_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, !Contains.Substring("Server"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_Server_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Server"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_Server()
        {
            var settings = new DatabaseConnectionSettings
            {
                Server = "Test"
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Server=Test"));
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
        public void ToInsecureConnectionString_does_not_include_Database_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, !Contains.Substring("Database"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_Database_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Database"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_Database()
        {
            var settings = new DatabaseConnectionSettings
            {
                Database = "Test"
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Database=Test"));
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
        public void ToInsecureConnectionString_does_not_include_ConnectionTimeout_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, !Contains.Substring("Connection Timeout"));
        }

        [Test]
        public void CreateSqlConnection_does_not_include_ConnectionTimeout_if_not_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Connection Timeout"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_ConnectionTimeout()
        {
            var settings = new DatabaseConnectionSettings
            {
                ConnectionTimeoutInSeconds = 30
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Connection Timeout=30"));
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
        public void ToInsecureConnectionString_sets_Integrated_Security_if_no_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Integrated Security=True"));
        }

        [Test]
        public void CreateSqlConnection_sets_Integrated_Security_if_no_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings();

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, Contains.Substring("Integrated Security=True"));
        }

        [Test]
        public void ToInsecureConnectionString_does_not_set_Integrated_Security_if_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials()
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, !Contains.Substring("Integrated Security"));
        }

        [Test]
        public void CreateSqlConnection_does_not_set_Integrated_Security_if_credentials_specified()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    User = string.Empty,
                    Password = string.Empty.ToSecureString()
                }
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.ConnectionString, !Contains.Substring("Integrated Security"));
        }

        [Test]
        public void ToInsecureConnectionString_sets_User()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    User = "TestUser",
                }
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("User Id=TestUser"));
        }

        [Test]
        public void CreateSqlConnection_sets_SqlCredentials_User()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    User = "User",
                    Password = string.Empty.ToSecureString()
                }
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.Credential.UserId, Is.EqualTo(settings.Credentials.User));
        }

        [Test]
        public void ToInsecureConnectionString_sets_Password()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    Password = "Test".ToSecureString()
                }
            };

            var connectionString = settings.ToInsecureConnectionString();

            Assert.That(connectionString, Contains.Substring("Password=Test"));
        }

        [Test]
        public void CreateSqlConnection_sets_SqlCredentials_Password()
        {
            var settings = new DatabaseConnectionSettings
            {
                Credentials = new Credentials
                {
                    User = string.Empty,
                    Password = "Test".ToSecureString()
                }
            };

            var sqlConnection = settings.CreateSqlConnection();

            Assert.That(sqlConnection.Credential.Password, Is.EqualTo(settings.Credentials.Password));
        }
    }
}
