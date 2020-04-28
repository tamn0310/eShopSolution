namespace eShopSolution.Api.Application
{
    public class ApiResultSuccess<T> : ApiResult<T>
    {
        public ApiResultSuccess(T data)
        {
            IsSuccessed = true;
            Data = data;
        }

        public ApiResultSuccess()
        {
            IsSuccessed = true;
        }
    }
}