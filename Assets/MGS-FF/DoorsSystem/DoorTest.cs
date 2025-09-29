using UnityEngine;

namespace DoorsSystem
{
    public class DoorTest : MonoBehaviour
    {
        [SerializeField] private int _testCardLvl = 4;
        [SerializeField] private DoorView _prefab;
        [SerializeField] private LayerMask _layerMask;
        
        void Start()
        {
            var door = new Door(
                2,
                Instantiate(_prefab, transform.position, Quaternion.identity),
                _layerMask,
                DoorCanOpen);
            
            door.Init();
        }

        private bool DoorCanOpen(int lvl)
        {
            return lvl <= _testCardLvl;
        }
    }
}