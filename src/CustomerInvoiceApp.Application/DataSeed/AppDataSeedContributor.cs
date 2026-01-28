using Abp.Dependency;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace CustomerInvoiceApp.DataSeed
{
	public class AppDataSeedContributor : IDataSeedContributor, ITransientDependency
	{
		private readonly IIdentityRoleRepository _roleRepository;
		private readonly IdentityRoleManager _roleManager;
		private readonly IPermissionDataSeeder _permissionDataSeeder;
		private readonly Volo.Abp.Guids.IGuidGenerator _guidGenerator;

		public AppDataSeedContributor(IIdentityRoleRepository roleRepository, IdentityRoleManager roleManager, IPermissionDataSeeder permissionDataSeeder, Volo.Abp.Guids.IGuidGenerator guidGenerator)
		{
			_roleRepository = roleRepository;
			_roleManager = roleManager;
			_permissionDataSeeder = permissionDataSeeder;
			_guidGenerator = guidGenerator;
		}
		public async Task SeedAsync(DataSeedContext context)
		{
			Console.WriteLine(">>> AppDataSeedContributor started...");
			var adminRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
			if (adminRole == null)
			{
				var newRole1 = new IdentityRole(_guidGenerator.Create(), "Admin", context?.TenantId );

				var result = await _roleManager.CreateAsync(newRole1);
				if (!result.Succeeded)
				{
					throw new Exception("Failed to create Admin role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
				}

				adminRole = newRole1;
			}

			await _permissionDataSeeder.SeedAsync(
					providerName: RolePermissionValueProvider.ProviderName,
					providerKey: adminRole.Name,
					grantedPermissions: ["Customers.Delete"],
					tenantId: context?.TenantId
			);

			var staffRole = await _roleRepository.FindByNormalizedNameAsync("STAFF");
			if (staffRole == null)
			{
				var newRole2 = new IdentityRole(_guidGenerator.Create(), "Staff", context?.TenantId);

				var result = await _roleManager.CreateAsync(newRole2);
				if (!result.Succeeded)
				{
					throw new Exception("Failed to create Staff role: " + string.Join(", ", result.Errors.Select(e => e.Description)));
				}

				staffRole = newRole2;
			}


			await _permissionDataSeeder.SeedAsync(
					providerName: RolePermissionValueProvider.ProviderName,
					providerKey: staffRole.Name,
					grantedPermissions: ["Invoices.Create"],
					tenantId: context?.TenantId
			);
			Console.WriteLine(">>> AppDataSeedContributor completed...");
		}
	}
}
