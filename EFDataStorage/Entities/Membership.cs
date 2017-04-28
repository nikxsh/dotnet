using System;
using System.ComponentModel.DataAnnotations;

namespace EFDataStorage.Entities
{
    public class Membership
    {
        [Key]
        public Guid MembershipId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual User User { get; set; }
    }
}
