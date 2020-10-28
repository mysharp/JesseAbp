﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace AbpLoanDemo.Identity.Web
{
    [Dependency(ReplaceServices = true)]
    public class IdentityBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Identity";
    }
}
