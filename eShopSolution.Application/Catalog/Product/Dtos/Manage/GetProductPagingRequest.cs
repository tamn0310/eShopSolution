using eShopSolution.Application.DtosCommon;
using System.Collections.Generic;

namespace eShopSolution.Application.Catalog.Product.Dtos.Manage
{
    /// <summary>
    /// Lấy danh sách sản phẩm, phân tranng - admin
    /// </summary>
    public class GetProductPagingRequest : PagingRequestBase
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