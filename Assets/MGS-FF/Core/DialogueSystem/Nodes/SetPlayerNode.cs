using UnityEngine;

namespace DialogueSystem
{
    public class SetPlayerNode : EditorDialogueNode
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public SetPlayerNode(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}