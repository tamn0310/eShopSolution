using eShopSolution.Dtos.Common;
using System.Collections.Generic;

namespace eShopSolution.Dtos.Catalog.Products
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

        /// <summary>
        /// Danh sách danh mục sản phẩm
        /// </summary>
        public List<int> CategoryIds { get; set; }
    }
}