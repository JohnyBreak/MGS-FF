using System;

namespace UI.MVP
{
    public interface IView : IDisposable
    {
        void Initialize();
        void SetInputActive(bool value);
        void SetActive(bool value);
    }
}