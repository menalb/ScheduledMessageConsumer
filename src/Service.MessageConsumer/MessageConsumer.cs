using System;
using System.Threading.Tasks;

namespace Service.MessageConsumer
{
    public interface MessageConsumer
    {
        Task Process(Action<Exception> onFailure, Action<string> logInfo);
    }
}