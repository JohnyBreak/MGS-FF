using System;

namespace AlertSystem
{
    public interface IState : IDisposable
    {
        public void Enter();
        public void Exit();
        public int GetKey();
    }
}