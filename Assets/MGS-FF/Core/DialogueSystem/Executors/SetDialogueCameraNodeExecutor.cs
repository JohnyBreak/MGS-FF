using UnityEngine;

namespace DialogueSystem
{
    public class SetDialogueCameraNodeExecutor : BaseExecutor<SetDialogueCameraNode>
    {
        private readonly Transform _camera;

        public SetDialogueCameraNodeExecutor(Transform camera)
        {
            _camera = camera;
        }

        protected override void OnExecute(SetDialogueCameraNode node)
        {
            _camera.SetPositionAndRotation(node.Position, node.Rotation);
        }
    }
}