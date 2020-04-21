namespace eShopSolution.Api.Application.Queries.Users
{
    /// <summary>
    /// param getall
    /// </summary>
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Search { get; set; }
    }
}