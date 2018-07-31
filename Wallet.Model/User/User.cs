using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Model.User
{
    public class User : IEntity
    {
        // NEWSEQUENTIALID()
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual Guid Id { get; set; }

        // Unique
        [StringLength(25, MinimumLength = 5)]
        [Required]
        public virtual string MobileNumber { get; set; }

        // Unique
        [StringLength(50, MinimumLength = 5)]
        public virtual string EmailAddress { get; set; }
    }
}
