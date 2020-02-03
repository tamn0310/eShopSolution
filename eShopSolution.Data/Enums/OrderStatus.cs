namespace eShopSolution.Data.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// Trong tiến trình
        /// </summary>
        InProgress,

        /// <summary>
        /// Đã xác nhận đơn hàng
        /// </summary>
        Confirmed,

        /// <summary>
        /// Đnag đi giao
        /// </summary>
        Shipping,

        /// <summary>
        /// Giao thành công
        /// </summary>
        Success,

        /// <summary>
        /// Đã hủy đơn hàng
        /// </summary>
        Canceled
    }
}