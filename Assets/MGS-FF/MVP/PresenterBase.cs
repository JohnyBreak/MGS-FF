namespace UI.MVP
{
    public abstract class PresenterBase<TModel, TView> : IPresenter<TModel, TView>
        where TModel : IModel
        where TView : IView
    {
        public TModel Model { get; }
        public TView View { get; }
        public bool IsInitialized { get; private set; }
        protected PresenterBase(TModel model, TView view)
        {
            Model = model;
            View = view;
        }
        
        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }
            try
            {
                OnInitialize();
            }
            finally
            {
                IsInitialized = true;
            }
        }

        public void Dispose()
        {
            if (!IsInitialized)
            {
                return;
            }

            try
            {
                OnDispose();
            }
            finally
            {
                IsInitialized = false;
            }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnDispose() { }


        IView IPresenter.View => View;
        IModel IPresenter.Model => Model;
    }
}