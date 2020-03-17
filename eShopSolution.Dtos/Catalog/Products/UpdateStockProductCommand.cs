namespace eShopSolution.Dtos.Catalog.Products
{
    /// <summary>
    /// Cập nhật lại số lượng sản phẩm
    /// </summary>
    public class UpdateStockProductCommand
    {
        public int Id { get; set; }

        /// <summary>
        /// số lượng sản phẩm muốn cập nhật
        /// </summary>
        public int Quantity { get; set; }
    }
}