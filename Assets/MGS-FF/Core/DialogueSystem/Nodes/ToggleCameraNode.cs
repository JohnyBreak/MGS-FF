namespace DialogueSystem
{
    public class ToggleCameraNode : EditorDialogueNode
    {
        public readonly bool Toggle;

        public ToggleCameraNode(bool toggle)
        {
            Toggle = toggle;
        }
    }
}