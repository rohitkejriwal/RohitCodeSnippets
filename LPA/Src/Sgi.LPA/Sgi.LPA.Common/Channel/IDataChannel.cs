using Sgi.LPA.Common.Models.Chat;
namespace Sgi.LPA.Common.Channel
{
    public interface IDataChannel
    {
        SearchQueryResonse SendQuery(NLP.Intent fillIntent);
    }
}