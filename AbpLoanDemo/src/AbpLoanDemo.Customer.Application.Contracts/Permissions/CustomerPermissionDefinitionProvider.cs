using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class CustomerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var customerGroup = context.AddGroup(CustomerPermissions.GroupName, L("Permission:CustomerStore"), MultiTenancySides.Both);

            var customerPermissions =
                customerGroup.AddPermission(CustomerPermissions.Customer.Default, L("Permission:CustomerStore_Customer"), MultiTenancySides.Both);

            customerPermissions.AddChild(CustomerPermissions.Customer.Create, L("Permission:CustomerStore_Customer_Create"));
            customerPermissions.AddChild(CustomerPermissions.Customer.AddLinkman, L("Permission:CustomerStore_Customer_AddLinkman"));
        }

        private LocalizableString L(string name)
        {
            return null;
        }
    }
}