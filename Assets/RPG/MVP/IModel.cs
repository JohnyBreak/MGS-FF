using System;

namespace UI.MVP
{
    public interface IModel : IDisposable
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}