namespace WebApiServices.Mapper
{
    public static class LoginMapper
    {
        public static Models.LoginResponse BuildAPILoginResponse(this EFDataStorage.Helper.LoginResponse response)
        {
            return new Models.LoginResponse
            {
                IsAuthenticated = response.IsAuthenticated,
                UserCurrentStatus = (Models.UserStatus)response.UserCurrentStatus,
                UserId = response.UserId
            };
        }
    }
}