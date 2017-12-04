#region Using namespaces
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
#endregion

namespace AutoCorrectAPI
{
    /// <summary>
    /// Product controller. Contains CURD operations on product
    /// </summary>
    public class SpellCheckController : ApiController
    {
        public string Check(int id)
        {
            string text = "tst";
            return CorrectText(text);
        }

        public string Check([FromBody]string text)
        {
            return CorrectText(text);
        }

        private string CorrectText(string text)
        {
            try
            {
                AutoCorrect.AutoCorrect autoCorrect = new AutoCorrect.AutoCorrect();
                var correctedText = autoCorrect.CorrectText(text);

                return correctedText;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}