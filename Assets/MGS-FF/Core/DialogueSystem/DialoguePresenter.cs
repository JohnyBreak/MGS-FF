using UI.MVP;
using UniRx;

namespace DialogueSystem
{
    public class DialoguePresenter : PresenterBase<DialogueModel, DialogueView>
    {
        private CompositeDisposable _cd = new();
        
        public DialoguePresenter(DialogueModel model, DialogueView view) : base(model, view)
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Model.Initialize();
            View.Initialize();
            View.SetActive(false);
            
            Model.Text
                .Skip(1)
                .Subscribe(SetText)
                .AddTo(_cd);
        }

        private void SetText(string text)
        {
            View.ShowPhrase(text);
        }

        public void Stop()
        {
            View.SetActive(false);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _cd.Dispose();
        }
    }
}

