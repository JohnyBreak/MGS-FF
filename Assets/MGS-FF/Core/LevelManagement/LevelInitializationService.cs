using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public class LevelInitializationService : ILevelInitializationService
    {
        public async UniTask Initialize(LevelInitializationEntryPoint point)
        {
            //point.GetConfig();
        }
    }
}