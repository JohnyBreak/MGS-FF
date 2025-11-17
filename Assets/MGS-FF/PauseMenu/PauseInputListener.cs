using UnityEngine;
using Infrastructure.ServiceLocator;

public class PauseInputListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // change game state to pause
            // enable pause canvas
            ServiceLocator.Get<PauseCanvas>().Toggle();
        }
    }
}
