using UnityEngine;

namespace LevelManagement
{
    public class LevelInitializationEntryPoint : MonoBehaviour
    {
        [SerializeField] private string _levelKey;
        
        public string GetLevelKey()
        {
            return _levelKey;
        }
    }
}

