using Abp.Dependency;
using Abp.Domain.Uow;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace CustomerInvoiceApp.DataSeed
{
	public class AppDataSeedContributor : IDataSeedContributor, ITransientDependency
	{
		private readonly IIdentityRoleRepository _roleRepository;
		private readonly IdentityRoleManager _roleManager;

		public AppDataSeedContributor(
			IIdentityRoleRepository roleRepository,
			IdentityRoleManager roleManager)
		{
			_roleRepository = roleRepository;
			_roleManager = roleManager;
			//_rolePermissionRepository = rolePermissionRepository;
		}

		[UnitOfWork]
		public async Task SeedAsync(DataSeedContext context)
		{
		//	// --- Admin Role ---
		//	var adminRole = await _roleManager.FindByNameAsync("Admin");
		//	if (adminRole == null)
		//	{
		//		adminRole = await _roleRepository.InsertAsync(
		//			new IdentityRole(Guid.NewGuid(), "Admin"), autoSave: true
		//		);
		//	}

		//	// Assign permissions via repository (string-based)
		//	var adminPermissions = new[]
		//	{
		//	"Customers.Delete",
		//	"Invoices.Create",
		//	"Invoices.Update",
		//	"Invoices.Delete"
		//};

		//	foreach (var permName in adminPermissions)
		//	{
		//		if (!await _rolePermissionRepository.ExistsAsync(adminRole.Id, null, permName))
		//		{
		//			await _rolePermissionRepository.InsertAsync(
		//				new IdentityRolePermission(Guid.NewGuid(), adminRole.Id, null, permName)
		//			);
		//		}
		//	}

		//	// --- Staff Role ---
		//	//var staffRole = await _roleRepository.FindAsync(r => r.Name == "Staff");
		//	var staffRole = await _roleManager.FindByNameAsync("Staff");
		//	if (staffRole == null)
		//	{
		//		staffRole = await _roleRepository.InsertAsync(
		//			new IdentityRole(Guid.NewGuid(), "Staff"), autoSave: true
		//		);
		//	}

		//	var staffPermissions = new[]
		//	{
		//	"Invoices.Create",
		//	"Invoices.Update"
		//};

		//	foreach (var permName in staffPermissions)
		//	{
		//		if (!await _rolePermissionRepository.ExistsAsync(staffRole.Id, null, permName))
		//		{
		//			await _rolePermissionRepository.InsertAsync(
		//				new IdentityRolePermission(Guid.NewGuid(), staffRole.Id, null, permName)
		//			);
		//		}
		//	}
		}
	}
}
