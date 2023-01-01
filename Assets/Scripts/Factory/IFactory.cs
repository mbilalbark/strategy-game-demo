using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Factroy
{
    public interface IFactory<TValue>
    {
        public TValue Create();
    }

    public interface IFactory<in TParam1, out TValue>
    {
        public TValue Create(TParam1 param1);
    }

    public interface IFactory<in TParam1, in TParam2, out TValue>
    {
        public TValue Create(TParam1 param1, TParam2 param2);
    }
}
