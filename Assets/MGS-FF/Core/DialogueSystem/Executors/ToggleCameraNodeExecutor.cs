using UnityEngine;

namespace DialogueSystem
{
    public class ToggleCameraNodeExecutor : INodeExecutor
    {
        private readonly GameObject _camera;

        public ToggleCameraNodeExecutor(GameObject camera)
        {
            _camera = camera;
        }
        
        public void Execute(BaseDialogueNode node)
        {
            
            if (node is not ToggleCameraNode targetNode)
            {
                return;
            }
            
            _camera.SetActive(targetNode.Toggle);
        }
    }
}