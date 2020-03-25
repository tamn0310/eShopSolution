using Microsoft.AspNetCore.Http;
using System;

namespace eShopSolution.Api.Application.Commands.ProductImages
{
    /// <summary>
    /// Dto: product image
    /// </summary>
    public class ProductImageViewModel
    {
        /// <summary>
        /// Id hình ảnh
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// map với id sản phẩm tương ứng
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Caption của ảnh
        /// </summary>
        public string Alt { get; set; }

        /// <summary>
        /// Hình ảnh mặc định
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Thứ tự hình ảnh
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Kích thước ảnh
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate{ get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}