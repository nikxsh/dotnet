using System;

namespace WebApiServices.Models
{
    public class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool IsAuthenticated { get; set; }

        public int? RemainingLoginAttempts { get; set; }

        public string Token { get; set; }

        public string JWToken { get; set; }

        public bool IsFirstLogin { get; set; }

        public Guid UserId { get; set; }

        public bool PasswordExpired { get; set; }

        public UserStatus UserCurrentStatus { get; set; }
    }

    public enum UserStatus
    {
        Active, Deactive, Locked
    }
}