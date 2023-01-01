using STGD.Core.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.ObjectPooling
{
    public interface IPool
    {

    }
    public interface IPool<T> where T: IInitialize<T>
    {
        public T AddObjectToPool();
        public void RemoveObjectoFromPool(T obj);
    }

    public interface IPool<TParam, TValue> where TValue : IInitialize<TParam, TValue>
    {
        public TValue AddObjectToPool(TParam param);
        public void RemoveObjectoFromPool(TValue value);
    }

    public interface IPool<TParam1, TParam2,  TValue> where TValue : IInitialize<TParam1, TParam2, TValue>
    {
        public TValue AddObjectToPool(TParam1 param1, TParam2 param2);
        public void RemoveObjectoFromPool(TValue value);
    }
}
