namespace Application.RestApi.Model.Logger
{
    public class LogRestRequestModel
    {
        public string Type { get; set; }
        public string TransactionId { get; set; }
        public string Log { get; set; }
        public string User { get; set; }
    }
}