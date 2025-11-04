using UnityEngine;

public class GranadeTest : MonoBehaviour
{
    [SerializeField] private ChaffGranade _prefab;
    [SerializeField] private float _radius; 
    [SerializeField] private float _time;
    [SerializeField] private LayerMask _mask;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            var g = Instantiate(_prefab, transform.position, Quaternion.identity);
            g.Init(_radius, _time, _mask);
            g.Activate();
        }
    }
}
