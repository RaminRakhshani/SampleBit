using Bit.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wallet.Model.User
{
    public class Customer : IEntity
    {
        [Range(0, double.MaxValue)]
        public virtual int MaxTransactionsPerDay { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
