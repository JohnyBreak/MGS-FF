using System;

namespace SurveillanceCameraSystem
{
    public interface IState : IDisposable
    {
        void Enter();
        void Exit();
        int GetKey();
    }
}