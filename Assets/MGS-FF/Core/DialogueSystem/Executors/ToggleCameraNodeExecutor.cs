using UnityEngine;

namespace DialogueSystem
{
    public class ToggleCameraNodeExecutor : BaseExecutor<ToggleCameraNode>
    {
        private readonly GameObject _camera;

        public ToggleCameraNodeExecutor(GameObject camera)
        {
            _camera = camera;
        }
        
        protected override void OnExecute(ToggleCameraNode node)
        {
            _camera.SetActive(node.Toggle);
        }
    }
}