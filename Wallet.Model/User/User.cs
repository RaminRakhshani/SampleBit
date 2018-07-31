using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Model.User
{
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual Guid Id { get; set; }

        [StringLength(25, MinimumLength = 5)]
        [Required]
        public virtual string MobileNumber { get; set; }

        [StringLength(50, MinimumLength = 5)]
        public virtual string EmailAddress { get; set; }
    }
}
