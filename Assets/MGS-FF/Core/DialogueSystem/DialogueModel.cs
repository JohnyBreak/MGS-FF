using UI.MVP;
using UniRx;

namespace DialogueSystem
{
    public class DialogueModel : ModelBase
    {
        public ReactiveProperty<string> Text;
        protected override void OnInitialize()
        {
            base.OnInitialize();
            Text = new ReactiveProperty<string>("");
        }
    }
}