using System;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCamera : IDisposable
    {
        public event Action TargetSpottedEvent;
        public event Action TargetLostEvent;
        
        private StateMachine _stateMachine;

        public SurveillanceCamera(SurveillanceCameraView view, 
            float angle,
            float followSpeed,
            float patrolTime)
        {
            _stateMachine = new StateMachine(
                view,
                OnSpotted,
                OnLost,
                angle,
                followSpeed,
                patrolTime);
        }

        private void OnSpotted()
        {
            TargetSpottedEvent?.Invoke();
        }
        
        private void OnLost()
        {
            TargetLostEvent?.Invoke();
        }
        
        public void Dispose()
        {
            _stateMachine.Dispose();
        }
    }
}