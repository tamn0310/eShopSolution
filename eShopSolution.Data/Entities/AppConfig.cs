namespace eShopSolution.Data.Entities
{
    /// <summary>
    /// Bảng cấu hình
    /// </summary>
    public class AppConfig : BaseEntities
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// giá trị
        /// </summary>
        public string Value { get; set; }
    }
}