using IOCInfrastructure;
using Sgi.LPA.Common.Helper;
using Sgi.LPA.Common.Models.Http;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.UAM;
using System.Diagnostics;

namespace Sgi.LPA.Common.Utilities
{
    public static class DIExtensions
    {
        public static void UpdateTransactionStep(this IServiceTransactionData serviceRequestData)
        {
            int step = serviceRequestData.GetTransactionStep();
            serviceRequestData.Set(LPAConsts.TransactionStepKey, (++step));
        }

        public static string TransactionGetClientStateID(this IServiceTransactionData serviceRequestData)
        {
            var state = serviceRequestData.Get(LPAConsts.TransactionClientStateKey);
            if (state != null)
            {
                return (string)state;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetApplicationTransactionStep(this IServiceTransactionData serviceRequestData)
        {
            int step = serviceRequestData.Get<int>(LPAConsts.TransactionStepKey);
            var appId = ConfigHelper.GetAppSettingValueFromConfig("appid");
            return  string.Format("{0}-{1}", appId, step);
        }

        public static int GetTransactionStep(this IServiceTransactionData serviceRequestData)
        {
            int step = serviceRequestData.Get<int>(LPAConsts.TransactionStepKey);
            return step;
        }
        public static long TransactionLastStepDuration(this IServiceTransactionData serviceRequestData, int step)
        {
            var data = serviceRequestData.Get(string.Format("{0}-{1}", LPAConsts.TransactionStepDurationKey, --step));
            if (data != null)
            {
                return (long)data;
            }
            else
            {
                return 0;
            }
        }
        public static void UpdateTransactionStepDuration(this IServiceTransactionData serviceRequestData, int step, long duration)
        {
            serviceRequestData.Set(string.Format("{0}-{1}", LPAConsts.TransactionStepDurationKey, step), duration);
        }

        public static void SetUser(this IServiceTransactionData serviceRequestData, ILPAUAM userService)
        {
            serviceRequestData.Set(LPAConsts.UserKey, userService.GetUser(serviceRequestData.GetRequestHeader().AuthToken));
        }

        public static void SetStopwatch(this IServiceTransactionData serviceRequestData, Stopwatch stopwatch)
        {
            serviceRequestData.Set(LPAConsts.StopwatchKey, stopwatch);
        }

        public static Stopwatch GetStopwatch(this IServiceTransactionData serviceRequestData)
        {
            return serviceRequestData.Get<Stopwatch>(LPAConsts.StopwatchKey);
        }

        public static string GetAuthToken(this IServiceTransactionData serviceRequestData)
        {
            return (string)serviceRequestData.Get(LPAConsts.AuthToken);
        }

        public static LPAUser GetUser(this IServiceTransactionData serviceRequestData)
        {
            return serviceRequestData.Get<LPAUser>(LPAConsts.UserKey);
        }

        public static void SetRequestHeader(this IServiceTransactionData serviceRequestData, RequestHeader requestHeader)
        {
            serviceRequestData.Set(LPAConsts.DIRequestHeaderKey, requestHeader);
        }

        public static RequestHeader GetRequestHeader(this IServiceTransactionData serviceRequestData)
        {
            return serviceRequestData.Get<RequestHeader>(LPAConsts.DIRequestHeaderKey);
        }
    }
}