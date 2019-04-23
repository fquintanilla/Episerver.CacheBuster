using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Verndale.CacheBuster.Services
{
    public class Pagination<T> : IPagination<T>
    {
        /// <summary>
        /// Default page size.
        /// </summary>
        public const int DefaultPageSize = 20;

        private IList<T> results;
        private int totalItems;
        public int PageSize { get; }

        /// <summary>
        /// The query to execute.
        /// </summary>
        public IQueryable<T> Query { get; protected set; }

        public int PageNumber { get; }


        /// <summary>
        /// Creates a new instance of the <see cref="LazyPagination{T}"/> class.
        /// </summary>
        /// <param name="query">The query to page.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        public Pagination(IQueryable<T> query, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Query = query;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            TryExecuteQuery();

            foreach (var item in results)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Executes the query if it has not already been executed.
        /// </summary>
        protected void TryExecuteQuery()
        {
            //Results is not null, means query has already been executed.
            if (results != null)
                return;

            totalItems = Query.Count();
            results = ExecuteQuery();
        }

        /// <summary>
        /// Calls Queryable.Skip/Take to perform the pagination.
        /// </summary>
        /// <returns>The paged set of results.</returns>
        protected virtual IList<T> ExecuteQuery()
        {
            int numberToSkip = (PageNumber - 1) * PageSize;
            return Query.Skip(numberToSkip).Take(PageSize).ToList();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        public int TotalItems
        {
            get
            {
                TryExecuteQuery();
                return totalItems;
            }
        }

        public int TotalPages => (int) Math.Ceiling(((double) TotalItems) / PageSize);

        public int FirstItem
        {
            get
            {
                TryExecuteQuery();
                return ((PageNumber - 1) * PageSize) + 1;
            }
        }

        public int LastItem => FirstItem + results.Count - 1;

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}