

namespace Microsoft.Samples.NLayerApp.Infrastructure.Data.MainBoundedContext.UnitOfWork.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Microsoft.Samples.NLayerApp.Domain.MainBoundedContext.BankingModule.Aggregates.BankAccountAgg;

    /// <summary>
    /// The entity type configuration
    /// </summary>
    class BankAccountEntityTypeConfiguration
        :EntityTypeConfiguration<BankAccount>
    {
        public BankAccountEntityTypeConfiguration()
        {
            //key and properties

            this.HasKey(ba => ba.Id);

            this.Property(ba => ba.Balance)
                .HasPrecision(14, 2);

            //associations
            this.HasRequired(ba => ba.Customer)
                .WithMany()
                .HasForeignKey(ba => ba.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasMany(ba => ba.BankAccountActivity)
                .WithRequired()
                .HasForeignKey(ba => ba.BankAccountId)
                .WillCascadeOnDelete(true);

        }
    }
}
