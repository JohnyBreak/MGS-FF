using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCameraTest : MonoBehaviour
    {
        [SerializeField] private SurveillanceCameraView _view1;
        [SerializeField] private SurveillanceCameraView _view2;
        [SerializeField] private SurveillanceCameraView _view3;

        public SurveillanceCamera[] Cameras => new SurveillanceCamera[]
        {
            new SurveillanceCamera(_view1, 65, 3, 4),
            new SurveillanceCamera(_view2, 65, 3, 4),
            new SurveillanceCamera(_view3, 65, 3, 4)
        };
    }
}

