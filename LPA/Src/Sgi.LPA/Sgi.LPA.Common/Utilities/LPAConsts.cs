namespace Sgi.LPA.Common.Utilities
{
    public static class LPAConsts
    {
        public const string HttpHeaderClientKey = "lpa-clientid";
        public const string HttpHeaderSessionId = "lpa-sessionid";
        public const string HttpHeaderTnxId = "lpa-transactionid";
        public const string DIRequestHeaderKey = "requestheaderkey";
        public const string HttpHeaderAuthorization = "Authorization";
        public const string UserKey = "user";
        public const string TransactionStepKey = "txnSteps";
        public const string TransactionClientStateKey = "clientstatekey";
        public const string HttpHeaderClientStateKey = "clientstatekey";

        public const string TransactionStepDurationKey = "txnStepDuration";
        public const string StopwatchKey = "stopwatch";
        public const int SearchQueryResonseNoneStatus = -1;
        public const int SearchQueryResonseSuccessStatus = 0;
        public const string DataChannelIndexDBSearchKey = "indexdbsearch";
        public const string DataChannelCommonKey = "commondatachannel";

        public const string ServiceRequestDataChatMessageKey = "_chatmessage";
        public const string AuthToken = "AuthToken";
        public const string LUISNoneIntent = "None";

        public const string MongoDBDefaultConfigKey = "defaultconfig";
        public const string MongoDBIndexDBConfigKey = "indexerconfig";
        public const string MongoDBUserDBConfigKey = "userconfig";
        public const string MongoDBIndexRepositoryKey = "indexdbrepository";
        public const string MongoDBChatRepositoryKey = "chatdbrepository";
        public const string MongoDBUserRepositoryKey = "userdbrepository";
        public const string DataCrawlerDbSourceKey = "Database";
        public const string WebCrawlerDataSourceKey = "WebCrawler";
    }
}