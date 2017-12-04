namespace Sgi.LPA.Common.NLP
{
    public interface INLPParser
    {
        Intent GetNLPResponse(string query, string contextId);
    }
}