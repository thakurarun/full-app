using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataStore.Entities
{
    public enum UserStatus
    {
        InActive, Active, Suspended, Deleted, Blocked
    }


    public class User
    {
        public Guid Id { get; set; }
        [ForeignKey("Profile")]
        public Guid ProfileId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Profile UserProfile { get; set; }
    }

    public class Profile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User User { get; set; }
    }
}
