using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AC = AutoCorrect;


namespace AutoCorrectWebAPI.Controllers
{
    public class AutoCorrectController : ApiController
    {
        [HttpGet]
        public string AutoCorrectString(string text)
        {
            return CorrectText(text);
        }

        private string CorrectText(string text)
        {
            try
            {
                AC.AutoCorrect autoCorrect = new AC.AutoCorrect();
                var correctedText = autoCorrect.CorrectText(text);

                return correctedText;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
