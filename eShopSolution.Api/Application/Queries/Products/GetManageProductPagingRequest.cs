using System.Collections.Generic;

namespace eShopSolution.Api.Application.Queries.Products
{
    /// <summary>
    /// Lấy danh sách sản phẩm, phân tranng - admin
    /// </summary>
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string Keyword { get; set; }
    }
}