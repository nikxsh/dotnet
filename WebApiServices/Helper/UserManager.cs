namespace WebApiServices.Helper
{
    public static class UserManager
    {
        public static T PrepareRequest<T>(T request) where T : RequestBase
        {
            request.Token = string.Empty;
            return request;
        }
    }
}