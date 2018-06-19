using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace TMDb.Core
{
    internal static class TaskExtensions
    {
        public static async Task<ApiResult<bool>> WithBusyIndicator(this Task self, BaseViewModel vm = null)
        {
            SetBusy(vm, true);

            try
            {
                await self.ConfigureAwait(false);
                return ApiResult.Create(true, true, HttpStatusCode.OK);
            }
            catch (ApiException ex)
            {
                return Error<bool>(ex);
            }
            catch (Exception ex)
            {
                return Error<bool>(ex);
            }
            finally
            {
                SetBusy(vm, false);
            }
        }

        public static async Task<ApiResult<T>> WithBusyIndicator<T>(this Task<T> self, BaseViewModel vm = null)
        {
            SetBusy(vm, true);

            try
            {
                var result = await self.ConfigureAwait(false);
                return ApiResult.Create(result, true, HttpStatusCode.OK);
            }
            catch (ApiException apiEx)
            {
                return Error<T>(apiEx);
            }
            catch (System.Exception ex)
            {
                return Error<T>(ex);
            }
            finally
            {
                SetBusy(vm, false);
            }
        }

        static void SetBusy(BaseViewModel vm, bool value)
        {
            if (vm != null) vm.IsBusy = value;
        }

        static ApiResult<T> Error<T>(ApiException apiEx)
        {
            switch (apiEx.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode, Resource.Unauthorized);
                case HttpStatusCode.NotFound:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode, Resource.NotFound);
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadRequest:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode,
                       JsonConvert.DeserializeObject<JObject>(apiEx.Content).Value<string>("status_message"));
                default:
                    return ApiResult.Create(default(T), false, apiEx.StatusCode, Resource.DefaultLoadError);
            }
        }

        static ApiResult<T> Error<T>(Exception ex) =>
            ApiResult.Create(default(T), false, HttpStatusCode.ExpectationFailed, Resource.DefaultLoadError);
    }
}
