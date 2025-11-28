using System;
using System.Collections.Generic;
using Collectables;
using Collectables.UI;
using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public class LevelInitializationService : ILevelInitializationService
    {
        private Dictionary<Type, IOperationHandler> _handlersMap;

        public LevelInitializationService(CollectableService collectableService)
        {
            var loot = new LootOperationHandler(collectableService);
            
            _handlersMap = new()
            {
                {loot.GetOperationType(), loot}
            };
        }

        public async UniTask Initialize(LevelInitializationEntryPoint point)
        {
            InitializationConfig config = GetConfig(point.GetLevelKey());
            await ResolveOperations(config);
        }

        private async UniTask ResolveOperations(InitializationConfig config)
        {
            foreach (var operation in config.Entries)
            {
                if (!_handlersMap.TryGetValue(operation.GetType(), out var handler))
                {
                    continue;
                }

                await handler.Handle(operation);
            }
        }

        private InitializationConfig GetConfig(string getLevelKey)
        {
            return new InitializationConfig();
        }
    }
}