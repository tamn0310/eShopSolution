namespace eShopSolution.Api.Application.Commands.Products.Update
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