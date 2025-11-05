using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerFactory : IInitable
{
    private AssetProvider _assetProvider;
    private string _key = "Player";
    private GameObject _playerPrefab;
    
    public PlayerFactory(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }
    
    public void Init()
    {
        
    }

    public async UniTask SpawnPlayer(Vector3 position)
    {
        _playerPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_key);
        
        Object.Instantiate(_playerPrefab, position, Quaternion.identity);
    }
}
