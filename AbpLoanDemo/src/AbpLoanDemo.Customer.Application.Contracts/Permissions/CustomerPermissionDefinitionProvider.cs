using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class CustomerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var customerGroup = context.AddGroup(CustomerPermissions.GroupName, L("Customer:Customer"), MultiTenancySides.Both);

            var customerPermissions =
                customerGroup.AddPermission(CustomerPermissions.Customer.Default, L("Customer:Customer"), MultiTenancySides.Both);

            customerPermissions.AddChild(CustomerPermissions.Customer.Create, L("Customer:Customer:Create"));
            customerPermissions.AddChild(CustomerPermissions.Customer.AddLinkman, L("Customer:Customer:AddLinkman"));
        }

        private LocalizableString L(string name)
        {
            return null;
        }
    }
}