namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class LoanPermissions
    {
        public const string GroupName = "Loan";

        public static class LoanRequest
        {
            public const string Default = GroupName + ".LoanRequest";
            public const string Create = Default + ".Create";
            public const string AddPartner = Default + ".AddPartner";
            public const string UpdateScore = Default + ".UpdateScore";
            public const string UpdateGuarantee = Default + ".UpdateGuarantee";
            public const string UpdateAmount = Default + ".UpdateAmount";
        }
    }
}