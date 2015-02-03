using System.Data.Common;
using System.Transactions;
using Microsoft.Its.Domain;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Spritely.Cqrs
{
    public sealed class Database<T> where T : class, IDatabase, new()
    {
        public static void ConfigureUnitOfWork(string connectionString)
        {
            UnitOfWork<T>.Commit = CommitUnitOfWork;
            UnitOfWork<T>.Reject = RejectUnitOfWork;

            UnitOfWork<T>.Create = (unitOfWork, setSubject) =>
            {
                var database = new T
                {
                    Transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted
                    })
                };

                database.CreateConnection(connectionString);

                setSubject(database);
            };
        }

        private static void RejectUnitOfWork(UnitOfWork<T> unitOfWork)
        {
            var connection = unitOfWork.Subject.Connection;
            if (connection != null)
            {
                connection.Dispose();
            }

            var transaction = unitOfWork.Subject.Transaction;
            if (transaction != null)
            {
                transaction.Dispose();
            }

            var exception = unitOfWork.Exception;
            if (exception != null)
            {
                unitOfWork.Subject.ReportException(exception);
            }
        }

        private static void CommitUnitOfWork(UnitOfWork<T> unitOfWork)
        {
            try
            {
                var transaction = unitOfWork.Subject.Transaction;

                using (transaction)
                {
                    transaction.Complete();
                }

                var connection = unitOfWork.Subject.Connection;
                using (connection)
                {
                    connection.Close();
                }
            }
            catch (DbException exception)
            {
                unitOfWork.RejectDueTo(exception);
            }
        }
    }
}
