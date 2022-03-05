using System;
using System.Collections.Generic;
using eCommerce.Common.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Security.Entities
{
    public class User : IdentityUser<int>, IIdentityUser<int>, IAuditable<int?>
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            UserRoles = new HashSet<UserRole>();
        }

        public string FullName => $"{FirstName} {LastName}".Trim();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserType { get; set; }
        public string UserPassword { get; set; }
        public int? CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModifierId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserLogin> Logins { get; set; }
        public ICollection<UserToken> Tokens { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}