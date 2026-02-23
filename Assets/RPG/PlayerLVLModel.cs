using UI.MVP;
using UniRx;

namespace RPG
{
    public class PlayerLVLModel : ModelBase
    {
        public IReactiveProperty<int> CurrentLVL { get; private set; } = new ReactiveProperty<int>(1);
        public IReactiveProperty<int> CurrentUpgradePoints { get; private set; } = new ReactiveProperty<int>(0);
        
        public void AddLVL(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            CurrentLVL.Value += amount;
        }
        
        public void AddUpgradePoints(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            CurrentUpgradePoints.Value += amount;
        }
        
        public bool TryReduceUpgradePoints(int amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            var current = CurrentUpgradePoints.Value;
            if ((current - amount) < 0)
            {
                return false;
            }

            CurrentUpgradePoints.Value -= amount;
            return true;
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}