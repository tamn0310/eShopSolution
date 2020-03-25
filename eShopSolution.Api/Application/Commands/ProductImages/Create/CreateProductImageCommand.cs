using Microsoft.AspNetCore.Http;

namespace eShopSolution.Api.Application.Commands.ProductImages.Create
{
    /// <summary>
    /// Dto: create product image
    /// </summary>
    public class CreateProductImageCommand
    {
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
        /// Image
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}