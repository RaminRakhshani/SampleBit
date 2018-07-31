using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.Model.User
{
    public class Operator : IEntity
    {
        // Unique
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public virtual string UserName { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public virtual string Password { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
