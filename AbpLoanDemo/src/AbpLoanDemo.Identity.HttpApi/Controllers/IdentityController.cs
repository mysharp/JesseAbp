using AbpLoanDemo.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpLoanDemo.Identity.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class IdentityController : AbpController
    {
        protected IdentityController()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}