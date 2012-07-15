using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.Pagination;

namespace ECom.Site.Core
{
	public static class EnumerableExtentions
	{
		public static IPagination<T> AsPagination<T>(this IEnumerable<T> source, int page, int pageSize, int totalCount)
		{
			return new CustomPagination<T>(source, page, pageSize, totalCount);
		}
	}
}