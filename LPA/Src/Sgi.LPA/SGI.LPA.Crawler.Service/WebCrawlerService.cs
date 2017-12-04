using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NReco.PhantomJS;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using SGI.LPA.Crawler.Service.Models;
using Sgi.Core.DBService;
using Sgi.MongoDBService;
using Sgi.MongoDBService.Interfaces;
using Newtonsoft.Json;
using SGI.LPA.Crawler.Service.Helper;
using IOCInfrastructure;

namespace SGI.LPA.Crawler.Service
{
    public class WebCrawlerService : ICrawlerService
    {
        private IDBRepository _dbService;
        private ILogHelper _logHelper;
        private const string _indexerCollectionName = "WebCrawlerData";

        public WebCrawlerService(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _dbService = serviceResolver.GetInstance<IDBRepository>("webCrawlerDbRepository");
        }

        public string GetImageBytesFromWebsite(IndexerModel indexer)
        {
            _logHelper.LogStep("Enter to WebCrawlerService.GetImageBytesFromWebsite method", this);

            CrawlerIntent crawlerIntent = null;
            string url, selector, fileName, screenshotPath;
            string imageBytes = string.Empty;

            try
            {
                crawlerIntent = GetCrawlerIntent(indexer);

                if (crawlerIntent != null)
                {
                    url = crawlerIntent.Url;
                    selector = crawlerIntent.Selector;
                    screenshotPath = ConfigHelper.GetAppSettingValueFromConfig("ScreenShotPath");

                    fileName = string.Format("{0}{1}{2}{3}", screenshotPath, indexer.Entities[0].Values[0], selector.Substring(1), DateTime.Now.ToShortDateString().Replace("/", ""));

                    TakeScreenShot(url, selector, fileName);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK;

                    fileName += ".png";

                    Bitmap image1 = (Bitmap)Image.FromFile(fileName, true);

                    ImageConverter converter = new ImageConverter();
                    imageBytes = Convert.ToBase64String((byte[])converter.ConvertTo(image1, typeof(byte[])));
                }
            }
            catch(Exception ex)
            {
                _logHelper.LogError(ex, "WebCrawlerService", "GetImageBytesFromWebsite", null, this);
                throw ex;
            }

            _logHelper.LogStep("Exit from WebCrawlerService.GetImageBytesFromWebsite method", this);
            return imageBytes;
        }

        public string GetStringValueFromWebsite(IndexerModel indexer)
        {
            throw new NotImplementedException();
        }

        private CrawlerIntent GetCrawlerIntent(IndexerModel indexer)
        {
            _logHelper.LogStep("Enter to WebCrawlerService.GetCrawlerIntent method", this);
            CrawlerIntent webCrawlerIntent = null;
            string jurisdiction = "ALC";

            List<FilterCriteria> filters = new List<FilterCriteria>() 
            { 
                new FilterCriteria() 
                { 
                    DataType = DataType.String.ToString(), 
                    Key = "Intent", 
                    Operator = Operators.EqualTo.ToString(), 
                    Value = indexer.Intent
                }
            };

            if (!string.IsNullOrEmpty(jurisdiction))
            {
                filters.Add(
                    new FilterCriteria()
                    {
                        DataType = DataType.StringList.ToString(),
                        Key = "Jurisdiction",
                        Operator = Operators.Contains.ToString(),
                        Value = jurisdiction
                    }
                    );
            }

            if (indexer.Entities.Count > 0 && !string.IsNullOrEmpty(indexer.Entities.First().Key))
            {
                filters.Add(
                    new FilterCriteria()
                    {
                        DataType = DataType.EntityValues.ToString(),
                        Key = "Entities.Key",
                        Operator = Operators.EqualTo.ToString(),
                        Value = indexer.Entities.First().Key
                    });

                filters.Add(
                    new FilterCriteria()
                    {
                        DataType = DataType.EntityValues.ToString(),
                        Key = "Entities.Values",
                        Operator = Operators.Contains.ToString(),
                        Value = indexer.Entities.First().Values.First()
                    });
            }

            try
            {
                var indexerJObjects = _dbService.GetData(_indexerCollectionName, filters);

                webCrawlerIntent = (from item in indexerJObjects
                                select JsonConvert.DeserializeObject<CrawlerIntent>(item.ToString())).First();

            }
            catch(Exception ex)
            {
                _logHelper.LogError(ex, "WebCrawlerService", "GetCrawlerIntent", null, this);
                webCrawlerIntent = null;
            }

            _logHelper.LogStep("Exit from WebCrawlerService.GetCrawlerIntent method", this);
            return webCrawlerIntent;
        }

        private void TakeScreenShot(string address, string selector, string targetFile)
        {
            _logHelper.LogStep("Enter to WebCrawlerService.TakeScreenShot method", this);
            if (!File.Exists(targetFile))
            {
                var phantomJS = new PhantomJS();
                string[] phantomArgs = { address, selector, targetFile };

                string scriptPath = @"..\Scripts\screenShot.js";
                //string scriptPath = ConfigHelper.GetAppSettingValueFromConfig("ScreenShotPath") + @"Scripts\ScreenShot.js";

                phantomJS.Run(scriptPath, phantomArgs);
            }
            _logHelper.LogStep("Exit from WebCrawlerService.TakeScreenShot method", this);
        }
    }
}
