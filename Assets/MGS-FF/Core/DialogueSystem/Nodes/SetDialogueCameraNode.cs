using UnityEngine;

namespace DialogueSystem
{
    public class SetDialogueCameraNode : EditorDialogueNode
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public SetDialogueCameraNode(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}