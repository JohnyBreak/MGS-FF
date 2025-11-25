using System;
using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public interface IOperationHandler
    {
        public Type GetOperationType();
        UniTask Handle(IOperation operation);
    }
}