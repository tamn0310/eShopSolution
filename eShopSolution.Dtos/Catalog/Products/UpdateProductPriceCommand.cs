namespace eShopSolution.Dtos.Catalog.Products
{
    /// <summary>
    /// Cập nhật lại giá tiền của sản phẩm
    /// </summary>
    public class UpdateProductPriceCommand
    {
        public int Id { get; set; }

        /// <summary>
        /// Số tiền mới muốn update
        /// </summary>
        public decimal Price { get; set; }
    }
}