using CustomerInvoiceApp.CustomerManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CustomerInvoiceApp.CustomerManagement
{
	[ConnectionStringName("Default")]
	public class CustomerManagementDbContext : AbpDbContext<CustomerManagementDbContext>
	{
		public DbSet<Customer> Customers { get; set; }

		public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Customer>(b =>
			{
				b.ToTable("Customers");
				b.HasKey(c => c.Id);
				b.Property(c => c.Name).IsRequired().HasMaxLength(200);
				b.Property(c => c.Email).IsRequired().HasMaxLength(200);
				b.Property(c => c.Phone).HasMaxLength(50);

				b.OwnsOne(c => c.BillingAddress, a =>
				{
					a.WithOwner();
					a.Property(x => x.Street).HasColumnName("Street").HasMaxLength(200).IsRequired();
					a.Property(x => x.City).HasColumnName("City").HasMaxLength(100).IsRequired();
					a.Property(x => x.PostalCode).HasColumnName("PostalCode").HasMaxLength(20).IsRequired();
				});
			});
		}
	}
}
