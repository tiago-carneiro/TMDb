using Refit;
using System.Globalization;

namespace TMDb.Core
{
    public abstract class BaseService
    {
        protected string Language => CultureInfo.CurrentUICulture.Name;

        protected IApiRestfull Api
            => RestService.For<IApiRestfull>(ConstantHelper.ApiUrl);
    }
}
