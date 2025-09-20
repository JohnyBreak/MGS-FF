using System.Collections.Generic;
using UnityEngine;

namespace Collectables
{
    public class CollectablesTest : MonoBehaviour
    {
        [SerializeField] private CollectableObjectView _pistolAmmoView;
        
        private CollectableResolver _resolver;
        private TestCollectableContainer _container;

        private void Awake()
        {
            _container = new TestCollectableContainer();
            _resolver = new CollectableResolver(new List<ICollector>()
            {
                new PistolAmmoCollector(_container)
            });


            var pistolAmmo = new PistolAmmoCollectable(15);
            
            //_resolver.Collect(pistolAmmo);
            //Debug.LogError(_container.PistolAmmo);


            Instantiate(_pistolAmmoView, new Vector3(5, 1, 5), Quaternion.identity).Init(_resolver, pistolAmmo);
        }
    }
}