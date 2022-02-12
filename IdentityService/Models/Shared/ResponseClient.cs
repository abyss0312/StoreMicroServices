namespace IdentityService.Models.Shared
{
    public class ResponseClient<T>
    {
        public int Code { get; set; }
        public bool validationResult { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
