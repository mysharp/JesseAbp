using System;
using System.Collections.Generic;
using System.Text;
using AbpLoanDemo.Identity.Localization;
using Volo.Abp.Application.Services;

namespace AbpLoanDemo.Identity
{
    /* Inherit your application services from this class.
     */
    public abstract class IdentityAppService : ApplicationService
    {
        protected IdentityAppService()
        {
            LocalizationResource = typeof(IdentityResource);
        }
    }
}
