using UI.MVP;
using UniRx;

namespace RPG
{
    public class PlayerXPModel : ModelBase
    {
        public IReactiveProperty<int> CurrentXP { get;} = new ReactiveProperty<int>(0);
        public IReactiveProperty<int> NextLvlXp { get;} = new ReactiveProperty<int>(100);
        
        public void AddXP(int amount)
        {
            if (amount <= 0)
            {
                return;
            }

            CurrentXP.Value += amount;
        }

        public void SetNextLvlXp(int newValue)
        {
            if (newValue <= NextLvlXp.Value)
            {
                return;
            }

            NextLvlXp.Value = newValue;
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