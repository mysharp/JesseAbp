using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;

namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class LoanPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var customerGroup = context.AddGroup(LoanPermissions.GroupName, null, MultiTenancySides.Both);

            var customerPermissions =
                customerGroup.AddPermission(LoanPermissions.LoanRequest.Default, null, MultiTenancySides.Both);

            customerPermissions.AddChild(LoanPermissions.LoanRequest.Create);
            customerPermissions.AddChild(LoanPermissions.LoanRequest.AddPartner);
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateScore);
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateGuarantee);
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateAmount);
        }
    }
}