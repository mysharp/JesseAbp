using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class LoanPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var customerGroup = context.AddGroup(LoanPermissions.GroupName, L("Permission:LoanStore"), MultiTenancySides.Both);

            var customerPermissions =
                customerGroup.AddPermission(LoanPermissions.LoanRequest.Default, L("Permission:LoanStore_LoanRequest"), MultiTenancySides.Both);

            customerPermissions.AddChild(LoanPermissions.LoanRequest.Create, L("Permission:LoanStore_LoanRequest_Create"));
            customerPermissions.AddChild(LoanPermissions.LoanRequest.AddPartner, L("Permission:LoanStore_LoanRequest_AddPartner"));
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateScore, L("Permission:LoanStore_LoanRequest_UpdateScore"));
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateGuarantee, L("Permission:LoanStore_LoanRequest_UpdateGuarantee"));
            customerPermissions.AddChild(LoanPermissions.LoanRequest.UpdateAmount, L("Permission:LoanStore_LoanRequest_UpdateAmount"));
        }

        private LocalizableString L(string name)
        {
            return null;
        }
    }
}