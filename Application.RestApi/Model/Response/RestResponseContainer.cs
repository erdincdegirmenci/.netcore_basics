
namespace Application.RestApi.Model.Response
{
    public class RestResponseContainer<T>
    {
        public T Response { get; set; }
        public bool IsSucceed { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string TransactionCode { get; set; }
    }
}
