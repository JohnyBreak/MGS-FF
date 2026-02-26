namespace DialogueSystem
{
    public class ShowTextDialogueNode : BaseDialogueNode
    {
        public readonly string Text;
        
        public ShowTextDialogueNode(string text)
        {
            Text = text;
        }
    }
}