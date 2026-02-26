using System;

namespace UI.MVP
{
    public abstract class ModelBase : IModel
    {
        public bool IsInitialized { get; private set; }
        private bool IsDisposing { get; set; }

        public ModelBase()
        {
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnDispose() { }

        public void Initialize()
        {
            if(IsInitialized)
            {
                return;
            }

            try
            {
                OnInitialize();
            }
            catch(Exception ex)
            {
                // Log ex
            }
            finally
            {
                IsInitialized = true;
            }
        }

        public void Dispose()
        {
            if(!IsInitialized)
            {
                throw new Exception($"Model: \"{GetTypeName()}\" is already disposed.");
            }

            if (IsDisposing)
            {
                return;
            }

            try
            {
                IsDisposing = true;
                OnDispose();
            }
            finally
            {
                IsInitialized = false;
                IsDisposing = false;
            }
        }

        private string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}