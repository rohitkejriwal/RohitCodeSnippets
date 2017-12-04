using Sgi.LPA.Common.Models.Grammer;

namespace Sgi.LPA.Common.NLP
{
    public interface IGrammer
    {
        AutoCorrectResonse AutoConnect(string scetance);
    }
}