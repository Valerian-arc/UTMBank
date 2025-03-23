using Domain.Entites;
using System.Data.Entity;

namespace Domain.Data.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext() : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
