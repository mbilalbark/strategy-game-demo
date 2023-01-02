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

    public static class Subscriber<TParam1, TParam2>
    {
        // This function subscribe to the events of the publisher
        public static void Subscribe(Publisher<TParam1, TParam2> p, Action<Publisher<TParam1, TParam2>, TParam1, TParam2> e)
        {
            // register Action with publisher event
            p.OnPublish += e;

        }

        // This function unsubscribe from the events of the publisher
        public static void Unsubscribe(Publisher<TParam1, TParam2> p, Action<Publisher<TParam1, TParam2>, TParam1, TParam2> e)
        {
            // unregister Action from publisher
            p.OnPublish -= e;
        }
    }
}
