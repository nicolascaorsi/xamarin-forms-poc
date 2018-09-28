using System;
using System.Threading.Tasks;

namespace Company.Modules.Message.UseCases
{
    public interface IUseCase
    {
        void Execute();
    }
    public interface IUseCaseAsync
    {
        Task ExecuteAsync();
    }
    public interface IUseCase<T>
    {
        T Execute();
    }
    public interface IUseCase<T, Params>
    {
        T Execute(Params parameters = default(Params));
    }
    public interface IUseCaseAsync<T>
    {
        Task<T> ExecuteAsync();
    }
}
