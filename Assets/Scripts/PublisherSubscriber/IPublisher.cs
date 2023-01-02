using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.PubSub
{
    public interface IPublisher<T>
    {
        public void Publish(T obj);
    }

    public interface IPublisher<TParam1, TParam2>
    {
        public void Publish(TParam1 param1, TParam2 param2);
    }
}
