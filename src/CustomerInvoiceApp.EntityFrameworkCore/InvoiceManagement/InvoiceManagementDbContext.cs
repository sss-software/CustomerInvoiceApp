using CustomerInvoiceApp.InvoiceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CustomerInvoiceApp.InvoiceManagement
{
	[ConnectionStringName("Default")]
	public class InvoiceManagementDbContext : AbpDbContext<InvoiceManagementDbContext>
	{
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceLine> InvoiceLines { get; set; }

		public InvoiceManagementDbContext(DbContextOptions<InvoiceManagementDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Invoice>(b =>
			{
				b.ToTable("Invoices");
				b.HasKey(i => i.Id);
				b.Property(i => i.CustomerId).IsRequired();
				b.Property(i => i.InvoiceDate).IsRequired();
				b.Property(i => i.Number).IsRequired().HasMaxLength(50); 

				b.HasMany(i => i.Lines)
				 .WithOne()
				 .HasForeignKey("InvoiceId")
				 .IsRequired()
				 .OnDelete(DeleteBehavior.Cascade);
			});

			builder.Entity<InvoiceLine>(b =>
			{
				b.ToTable("InvoiceLines");
				b.HasKey(l => l.Id);
				b.Property(l => l.ProductId).IsRequired();
				b.Property(l => l.Description).IsRequired().HasMaxLength(500);
				b.Property(l => l.Quantity).IsRequired();
				b.Property(l => l.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
			});
		}
	}
}
