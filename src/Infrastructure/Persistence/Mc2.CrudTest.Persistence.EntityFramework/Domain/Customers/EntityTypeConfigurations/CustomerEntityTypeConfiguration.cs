using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Infrastructure.Domain.Instruments
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Firstname).HasMaxLength(ConstantValues.MaximumFirstnameLength);
            builder.Property(x => x.Lastname).HasMaxLength(ConstantValues.MaximumLastnameLength);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property<bool>("IsDeleted")
                .HasDefaultValue(false);

            builder.Property<DateTimeOffset?>("DeletedAt")
                .IsRequired(false);

            builder.Ignore(x => x.DomainEvents);

            builder.HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);

            builder.ToTable<Customer>(nameof(Customer));
        }
    }
}
