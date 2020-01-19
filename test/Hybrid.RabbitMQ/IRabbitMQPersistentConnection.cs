using RabbitMQ.Client;

namespace Hybrid.RabbitMQ
{
    /// <summary>
    /// rabbitmq持久化连接
    /// </summary>
    public interface IRabbitMQPersistentConnection
    {
        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        IModel CreateModel();
    }
}