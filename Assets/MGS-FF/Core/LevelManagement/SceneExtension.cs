using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public static class SceneExtension
    {
        public static T GetComponentOnRootObject<T>(this Scene scene) where T : Object
        {
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                if (rootGameObject.TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }

            return null;
        }
    }
}

