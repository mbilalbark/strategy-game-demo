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

    public class Publisher<TParam1, TParam2> : IPublisher<TParam1, TParam2>
    {
        public Action<Publisher<TParam1, TParam2>, TParam1, TParam2> OnPublish;
        public void Publish(TParam1 param1, TParam2 param2)
        {
            if (OnPublish != null)
            {
                OnPublish(this, param1, param2);
            }
        }
    }
}
