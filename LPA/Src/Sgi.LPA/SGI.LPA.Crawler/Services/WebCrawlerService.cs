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

namespace SGI.LPA.Crawler.Services
{
    public class WebCrawlerService
    {
        public string CrawlWebsite(string url, string selector)
        {
            string targetFile = string.Format(@"..\ScreenShots\{0}",selector.Substring(1) + DateTime.Now.ToShortDateString().Replace("/",""));
            
            try
            {
                TakeScreenShot(url, selector, targetFile);

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;

                string screenshotPath = HttpContext.Current.Server.MapPath("..\\"+targetFile+".png"); ;

                Bitmap image1 = (Bitmap)Image.FromFile(screenshotPath, true);

                ImageConverter converter = new ImageConverter();
                var imageBytes = Convert.ToBase64String((byte[])converter.ConvertTo(image1, typeof(byte[])));

                return imageBytes;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        private void TakeScreenShot(string address, string selector, string targetFile)
        {
            var phantomJS = new PhantomJS();
            string[] phantomArgs = { address, selector, targetFile };

            string scriptPath = @"..\Scripts\screenShot.js";

            phantomJS.Run(scriptPath, phantomArgs);

        }
    }
}