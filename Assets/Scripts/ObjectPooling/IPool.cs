using Newtonsoft.Json.Linq;
using STGD.Core.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.ObjectPooling
{
    public interface IPool
    {
        void RemoveObjectoFromPool(object obj);
    }

    public interface IRemoveablePool<TValue> : IPool
    {
        void RemoveObjectoFromPool(TValue obj);
    }
    public interface IPool<T> : IRemoveablePool<T> where T: IInitialize<T>
    {
        T AddObjectToPool();
    }

    public interface IPool<TParam, TValue>: IRemoveablePool<TValue> where TValue : IInitialize<TParam, TValue>
    {
        TValue AddObjectToPool(TParam param);
    }

    public interface IPool<TParam1, TParam2,  TValue> : IRemoveablePool<TValue> where TValue : IInitialize<TParam1, TParam2, TValue>
    {
        TValue AddObjectToPool(TParam1 param1, TParam2 param2);
    }

    public interface IPool<TParam1, TParam2, TParam3, TValue> : IRemoveablePool<TValue> where TValue : IInitialize<TParam1, TParam2, TParam3, TValue>
    {
        TValue AddObjectToPool(TParam1 param1, TParam2 param2, TParam3 param3);
    }
}
