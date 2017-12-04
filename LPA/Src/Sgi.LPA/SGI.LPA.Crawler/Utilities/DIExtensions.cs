using IOCInfrastructure;
using SGI.LPA.Crawler.Models;
using SGI.LPA.Crawler.Service.Helper;
using SGI.LPA.Crawler.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SGI.LPA.Crawler.Utilities
{
    public static class DIExtensions
    {
        public static void UpdateTransactionStep(this IServiceTransactionData serviceRequestData)
        {
            int step = serviceRequestData.GetTransactionStep();
            serviceRequestData.Set(Constants.TransactionStepKey, (++step));
        }

        public static string TransactionGetClientStateID(this IServiceTransactionData serviceRequestData)
        {
            var state = serviceRequestData.Get(Constants.TransactionClientStateKey);
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
            int step = serviceRequestData.Get<int>(Constants.TransactionStepKey);
            var appId = ConfigHelper.GetAppSettingValueFromConfig("appid");
            return string.Format("{0}-{1}", appId, step);
        }

        public static int GetTransactionStep(this IServiceTransactionData serviceRequestData)
        {
            int step = serviceRequestData.Get<int>(Constants.TransactionStepKey);
            return step;
        }

        public static long TransactionLastStepDuration(this IServiceTransactionData serviceRequestData, int step)
        {
            var data = serviceRequestData.Get(string.Format("{0}-{1}", Constants.TransactionStepDurationKey, --step));
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
            serviceRequestData.Set(string.Format("{0}-{1}", Constants.TransactionStepDurationKey, step), duration);
        }

        public static void SetStopwatch(this IServiceTransactionData serviceRequestData, Stopwatch stopwatch)
        {
            serviceRequestData.Set(Constants.StopwatchKey, stopwatch);
        }

        public static Stopwatch GetStopwatch(this IServiceTransactionData serviceRequestData)
        {
            return serviceRequestData.Get<Stopwatch>(Constants.StopwatchKey);
        }

        public static void SetRequestHeader(this IServiceTransactionData serviceRequestData, RequestHeader requestHeader)
        {
            serviceRequestData.Set(Constants.DIRequestHeaderKey, requestHeader);
        }

        public static RequestHeader GetRequestHeader(this IServiceTransactionData serviceRequestData)
        {
            return serviceRequestData.Get<RequestHeader>(Constants.DIRequestHeaderKey);
        }
    }
}