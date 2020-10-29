namespace AbpLoanDemo.Customer.Application.Contracts.Permissions
{
    public class CustomerPermissions
    {
        public const string GroupName = "CustomerStore";

        public static class Customer
        {
            public const string Default = GroupName + ".Customer";
            public const string Create = Default + ".Create";
            public const string AddLinkman = Default + ".AddLinkman";
        }
    }
}