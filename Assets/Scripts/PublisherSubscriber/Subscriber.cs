using System;
using UnityEngine;

namespace STGD.Core.PubSub
{
    public static class Subscriber<T>
    {
        // This function subscribe to the events of the publisher
        public static void Subscribe(Publisher<T> p, Action<Publisher<T>, T> e)
        {
            // register Action with publisher event
            p.OnPublish += e; 

        }

        // This function unsubscribe from the events of the publisher
        public static void Unsubscribe(Publisher<T> p, Action<Publisher<T>, T> e)
        {
            // unregister Action from publisher
            p.OnPublish -= e;
        }
    }
}
