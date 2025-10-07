using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCameraTest : MonoBehaviour
    {
        [SerializeField] private SurveillanceCameraView _view1;

        public SurveillanceCamera[] Cameras => new SurveillanceCamera[]
        {
            new SurveillanceCamera(_view1, 65, 3, 4)
        };
    }
}

