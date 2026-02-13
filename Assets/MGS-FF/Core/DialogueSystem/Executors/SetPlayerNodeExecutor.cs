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

        protected override void OnExecute(SetPlayerNode node, INodeExecutionContext context)
        {
            _player.SetPositionAndRotation(node.Position, node.Rotation);
            context?.MoveNext();
        }
    }
}