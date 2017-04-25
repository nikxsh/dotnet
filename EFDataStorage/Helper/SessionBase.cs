using System;

namespace EFDataStorage.Helper
{
    public class SessionBase
    {
        public UserRef RequestingUser { get; set; }
        public Subscription Subscription { get; set; }
    }

    public class Subscription
    {
        public string DbServerKey { get; set; }
    }

    public class UserRef
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
