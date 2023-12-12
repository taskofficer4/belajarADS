using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Actris.Web.Models
{
    public class PagedList<T> : IPagedList<T>
    {
        private readonly List<T> _set;

        public PagedList(IEnumerable<T> input, int totalItems, int page, int pageSize)
        {
            _set = input.ToList();
            PageCount = (int)Math.Ceiling((double)totalItems / pageSize);
            TotalItemCount = totalItems;
            PageNumber = page;
            PageSize = pageSize;
            HasPreviousPage = page > 0;
            HasNextPage = page < PageCount - 1;
            IsFirstPage = page == 0;
            IsLastPage = page == PageCount - 1;
            FirstItemOnPage = 0;
            LastItemOnPage = _set.Count - 1;
        }

        public T this[int index] => _set[index];

        public int PageCount { get; }

        public int TotalItemCount { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }

        public bool IsFirstPage { get; }

        public bool IsLastPage { get; }

        public int FirstItemOnPage { get; }

        public int LastItemOnPage { get; }

        public int Count => _set.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        public IPagedList GetMetaData()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.GetEnumerator();
        }
    }
}