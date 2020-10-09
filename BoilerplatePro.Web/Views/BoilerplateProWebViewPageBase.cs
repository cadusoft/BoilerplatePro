using Abp.Web.Mvc.Views;

namespace BoilerplatePro.Web.Views
{
    public abstract class BoilerplateProWebViewPageBase : BoilerplateProWebViewPageBase<dynamic>
    {

    }

    public abstract class BoilerplateProWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BoilerplateProWebViewPageBase()
        {
            LocalizationSourceName = BoilerplateProConsts.LocalizationSourceName;
        }
    }
}