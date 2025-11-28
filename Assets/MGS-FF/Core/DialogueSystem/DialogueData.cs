using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueData// рализовать ienumerable / ienumerator
    {
        private List<string> _texts = new List<string>();

        public IReadOnlyCollection<string> Texts => _texts;
        
        public void AppendText(string text)
        {
            _texts.Add(text);
        }
    }
}