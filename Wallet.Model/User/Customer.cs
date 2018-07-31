using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wallet.Model.User
{
    public class Customer : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual Guid Id { get; set; }

        [Range(0, double.MaxValue)]
        public virtual int MaxTransactionsPerDay { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual Guid UserId { get; set; }
    }
}
