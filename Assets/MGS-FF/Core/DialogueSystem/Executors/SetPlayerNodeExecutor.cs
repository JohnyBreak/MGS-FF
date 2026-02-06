using UnityEngine;

namespace DialogueSystem
{
    public class SetPlayerNodeExecutor : BaseExecutor<SetPlayerNode>
    {
        private readonly Transform _player;

        public SetPlayerNodeExecutor(Transform player)
        {
            _player = player;
        }

        protected override void OnExecute(SetPlayerNode node)
        {
            _player.SetPositionAndRotation(node.Position, node.Rotation);
        }
    }
}