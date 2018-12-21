using System;
using System.Threading.Tasks;

namespace Service.Consumer
{
    public interface MessageConsumer
    {
        Task Process(Action<Exception> onFailure, Action<string> logInfo);
    }
}