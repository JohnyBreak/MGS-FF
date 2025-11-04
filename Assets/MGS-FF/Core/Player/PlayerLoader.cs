using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    private AssetProvider _assetProvider;
    private string _key = "Player";
    private GameObject _playerPrefab;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    
    private void Start()
    {
        _assetProvider = new AssetProvider();
    }

    private async UniTaskVoid Load()
    {
        _spawnedObjects.Add(_assetProvider.InstantiateSync(_key));
        _spawnedObjects.Add(_assetProvider.InstantiateSync(_key));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Load().Forget();
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            //foreach (var so in _spawnedObjects)
           // {
                _assetProvider.ReleaseInstance(_spawnedObjects[0]);
            //}
            _spawnedObjects.Clear();
        }
    }
}
