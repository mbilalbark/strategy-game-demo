using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Factroy
{
    public abstract class Factory<TValue> :MonoBehaviour, IFactory<TValue>
    {
        public abstract TValue Create();
    }

    public abstract class Factory<TParam1, TValue> : MonoBehaviour, IFactory<TParam1, TValue>
    {
        public abstract TValue Create(TParam1 param1);
    }

    public abstract class Factory<TParam1, TParam2, TValue> : MonoBehaviour, IFactory<TParam1, TParam2, TValue>
    {
        public abstract TValue Create(TParam1 param1, TParam2 param2);
    }
}
