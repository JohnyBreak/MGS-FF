using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerFactory : IInitable
{
    private AssetProvider _assetProvider;
    private string _key = "Player";
    private string _playerCameraKey = "PlayerCamera";
    private string _mainCameraKey = "MainCamera";
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
        var mainCameraPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_mainCameraKey);
        var mainCam= Object.Instantiate(mainCameraPrefab, position, Quaternion.identity);
        _playerPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_key);
        var cameraPrefab = await _assetProvider.LoadAssetAsync<GameObject>(_playerCameraKey);
        var player = Object.Instantiate(_playerPrefab, position, Quaternion.identity).GetComponent<Player>();
        var camera = Object.Instantiate(cameraPrefab, position, Quaternion.identity);
        
        var cm = camera.GetComponent<CinemachineFreeLook>();
        cm.Follow = player.transform;
        cm.LookAt = player.LookAt;
        player.Init(mainCam.transform);
    }
}
