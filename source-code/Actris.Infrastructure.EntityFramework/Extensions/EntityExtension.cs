using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Actris.Infrastructure.EntityFramework.Extensions
{
    public static class EntityExtension
    {
        public static string ToDateDisplay(this DateTime? date)
        {
            if (date == null)
            {
                return "";
            }
            return date.Value.ToString("dd-MMM-yyyy");
        }

        #region WithNoLock
        // source : https://dotnetdocs.ir/Post/38/implementing-nolock-in-entityframework-
        public static async Task<List<T>> ToListWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            List<T> result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.ToListAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }

        public static List<T> ToListWithNoLock<T>(this IQueryable<T> query)
        {
            List<T> result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    }))
            {
                result = query.ToList();
                scope.Complete();
            }
            return result;
        }

        public static int CountWithNoLock<T>(this IQueryable<T> query)
        {
            int result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    }))
            {
                result = query.Count();
                scope.Complete();
            }
            return result;
        }

        public static async Task<int> CountWithNoLockAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            int result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.CountAsync(cancellationToken);
                scope.Complete();
            }
            return result;
        }

        public static int SumWithNoLock<T>(this IQueryable<T> query, Expression<Func<T, int>> selector)
        {
            int result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    }))
            {
                result = query.Sum(selector);
                scope.Complete();
            }
            return result;
        }

        public static async Task<int> SumWithNoLockAsync<T>(this IQueryable<T> query, Expression<Func<T, int>> selector, CancellationToken cancellationToken = default)
        {
            int result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.SumAsync(selector, cancellationToken);
                scope.Complete();
            }
            return result;
        }

        public static async Task<int> SumWithNoLockAsync<T>(this IQueryable<T> query, Expression<Func<T, int?>> selector, CancellationToken cancellationToken = default)
        {
            int result = 0;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    },
                                    TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await query.SumAsync(selector, cancellationToken) ?? 0;
                scope.Complete();
            }
            return result;
        }

        public static T FirstOrDefaultWithNoLock<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    }))
            {
                result = query.FirstOrDefault(predicate);
                scope.Complete();
            }
            return result;
        }

        public static T FirstOrDefaultWithNoLock<T>(this IQueryable<T> query)
        {
            T result = default;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    new TransactionOptions()
                                    {
                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                    }))
            {
                result = query.FirstOrDefault();
                scope.Complete();
            }
            return result;
        }

        #endregion
    }
}
