namespace eShopSolution.Api.AppModels
{
    public class ApiResult<TEntity>
    {
        /// <summary>
        /// Thông báo
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Kết quả
        /// </summary>
        public TEntity Data { get; set; }
    }
}