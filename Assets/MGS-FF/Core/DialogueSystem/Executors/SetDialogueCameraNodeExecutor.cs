using UnityEngine;

namespace DialogueSystem
{
    public class SetDialogueCameraNodeExecutor : INodeExecutor
    {
        private readonly Transform _camera;

        public SetDialogueCameraNodeExecutor(Transform camera)
        {
            _camera = camera;
        }

        public void Execute(BaseDialogueNode node)
        {
            if (node is not SetDialogueCameraNode targetNode)
            {
                return;
            }

            _camera.SetPositionAndRotation(targetNode.Position, targetNode.Rotation);
        }
    }
}