using System;

namespace STGD.Core.PubSub
{
    public class Publisher<T>: IPublisher<T>
    {
        public Action<Publisher<T>, T> OnPublish;
        public void Publish(T obj)
        {
            if (OnPublish != null)
            {
                OnPublish(this, obj);
            }
        }
    }
}
