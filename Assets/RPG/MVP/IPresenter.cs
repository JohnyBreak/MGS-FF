namespace UI.MVP
{
    public interface IPresenter
    {
        public IModel Model { get; }
        public IView View { get; }
        void Initialize();
    }
    
    public interface IPresenter<out TModel, out TView> : IPresenter
        where TModel : IModel
        where TView : IView
    {
        new TModel Model { get; }
        new TView View { get; }
        bool IsInitialized { get; }
    }
}