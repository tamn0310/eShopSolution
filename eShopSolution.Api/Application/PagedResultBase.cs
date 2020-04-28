using System;

namespace eShopSolution.Api.Application
{
    public class PagedResultBase
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)Total / Limit;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}