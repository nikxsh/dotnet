namespace WebApiServices.Helper
{
    public class RequestBase
    {
        //public UserRef User { get; set; }

        public string Token { get; set; }
    }

    public class RequestBase<T> : RequestBase
    {
        public T Data { get; set; }

        public RequestBase() { }

        public RequestBase(T data)
        {
            Data = data;
        }

        public RequestBase(T data, string token)
        {
            Data = data;
            Token = token;
        }
    }
}