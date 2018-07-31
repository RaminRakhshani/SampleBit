using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Model.User
{
    public class Operator : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public virtual string UserName { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 3)]
        public virtual string Password { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual Guid UserId { get; set; }
    }
}
