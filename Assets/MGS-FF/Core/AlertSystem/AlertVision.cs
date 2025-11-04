using System;

namespace AlertSystem
{
    public class AlertVision
    {
        public event Action FoundEvent;
        public event Action LostEvent;
        private int _count;

        public void Increase()
        {

            if (_count == 0)
            {
                FoundEvent?.Invoke();
            }

            _count++;
        }

        public void Decrease()
        {
            _count--;
            
            if (_count > 0)
            {
                return;
            }

            if (_count < 0)
            {
                _count = 0;
            }
            
            LostEvent?.Invoke();
        }
    }
}
