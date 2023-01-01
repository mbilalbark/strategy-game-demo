using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.PubSub
{
    public interface IPublisher<T>
    {
        public void Publish(T obj);
    }
}
