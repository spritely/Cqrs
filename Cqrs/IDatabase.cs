using System;
using System.Data;
using System.Transactions;

namespace Spritely.Cqrs
{
    public interface IDatabase
    {
        TransactionScope Transaction { get; set; }

        IDbConnection Connection { get; }

        void CreateConnection(string connectionString);

        void ReportException(Exception exception);
    }
}
