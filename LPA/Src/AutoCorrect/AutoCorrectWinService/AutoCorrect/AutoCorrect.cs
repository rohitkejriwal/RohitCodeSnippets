using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoCorrect
{
    public class AutoCorrect
    {
        Application _wordApp;

        public AutoCorrect()
        {
            if (_wordApp == null)
                _wordApp = new Application { Visible = false };
        }

        public string CorrectText(string text)
        {
            string correctedString = CheckSpelling(text);

            return correctedString;
        }

        private string CheckSpelling(string testString)
        {
            Dictionary<string, List<string>> suggestions;
            int iErrorCount = 0;
            string correctedString;
            Document _wordDoc = null;
            try
            {
                if (testString.Length > 0)
                {
                    _wordDoc = _wordApp.Documents.Add();
                    _wordDoc.Words.First.InsertBefore(testString);
                    ProofreadingErrors we = _wordDoc.SpellingErrors;
                    iErrorCount = we.Count;

                    if (iErrorCount > 0)
                    {
                        suggestions = FindErroroneousWords(testString);
                        return CorrectSpelling(testString, suggestions);
                    }
                }
            }
            catch
            {
                correctedString = testString;
            }
            finally
            {
                if (_wordDoc != null)
                    Close(_wordDoc);
            }

            return testString;
        }

        private Dictionary<string, List<string>> FindErroroneousWords(string input)
        {

            List<string> words = input.Split(' ').ToList();
            List<string> suggestion = new List<string>();
            Dictionary<string, List<string>> spellingSuggestions = new Dictionary<string, List<string>>();
            Document doc = null;

            try
            {
                object template = Missing.Value;
                object newTemplate = Missing.Value;
                object documentType = Missing.Value;
                object visible = true;
                object optional = Missing.Value;


                foreach (var word in words)
                {
                    if (word.Length > 0)
                    {
                        doc = _wordApp.Documents.Add(ref template,
                           ref newTemplate, ref documentType, ref visible);

                        doc.Words.First.InsertBefore(word);

                        ProofreadingErrors we = doc.SpellingErrors;

                        if (we.Count > 0)
                        {
                            suggestion = new List<string>();

                            SpellingSuggestions splSuggestions = _wordApp.GetSpellingSuggestions(word);

                            foreach (SpellingSuggestion spellingSuggestion in splSuggestions)
                            {
                                suggestion.Add(spellingSuggestion.Name);
                            }

                            spellingSuggestions.Add(word, suggestion);
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                Close(doc, false);
            }

            return spellingSuggestions;
        }

        private string CorrectSpelling(string testString, Dictionary<string, List<string>> suggestions)
        {
            string correctedString = testString.Replace("  ", " ");

            suggestions = RankSuggestions(suggestions);

            foreach (var item in suggestions)
            {
                correctedString = correctedString.Replace(item.Key, item.Value[0]);
            }

            return correctedString;
        }

        private Dictionary<string, List<string>> RankSuggestions(Dictionary<string, List<string>> suggestions)
        {
            Dictionary<string, List<string>> rankedSuggestions = new Dictionary<string, List<string>>();

            foreach (var item in suggestions)
            {
                var sug = RankAsPerMatchingFirstAndLastCharacter(item.Key, item.Value);
                sug = RankAsPerMinimumEditDistance(item.Key, sug);

                rankedSuggestions.Add(item.Key, sug);
            }

            return rankedSuggestions;
        }

        private List<string> RankAsPerMatchingFirstAndLastCharacter(string word, List<string> suggestions)
        {
            List<string> filteredSuggestions = new List<string>();

            foreach (var suggestion in suggestions)
            {
                if ((suggestion.ToLower()[0] == word.ToLower()[0])
                                        && (suggestion.ToLower()[suggestion.Length - 1] == word.ToLower()[word.Length - 1]))
                {
                    filteredSuggestions.Add(suggestion);
                }
            }

            return filteredSuggestions;
        }

        private List<string> RankAsPerMinimumEditDistance(string word, List<string> suggestions)
        {
            return suggestions;
        }

        public void Close(Document _wordDoc, bool quitApplication = true)
        {
            object saveOptionsObject = WdSaveOptions.wdDoNotSaveChanges;

            ((Microsoft.Office.Interop.Word._Document)_wordDoc).Close(WdSaveOptions.wdDoNotSaveChanges);

            if (quitApplication)
            {
                ((Microsoft.Office.Interop.Word._Application)_wordApp).Quit(false);
            }
        }
    }
}
