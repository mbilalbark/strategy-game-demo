using System.Collections.Generic;
using STGD.Core.Factroy;
using UnityEngine;
using System;

namespace STGD.Core.ObjectPooling
{
    [Serializable]

    public abstract class PoolBase<TValue> : MonoBehaviour, IPool
    {

    }
    public abstract class Pool<T> : PoolBase<T>, IPool<T> where T : IInitialize<T>
    {
        public abstract IFactory<T> Factory { get; set; }
        private Stack<T> pool;

        private void OnEnable()
        {
            pool = new Stack<T>();
        }

        public T AddObjectToPool()
        {
            T obj = pool.Count > 0 ? pool.Pop() : Create();
            obj.Init(this);
            return obj;
        }

        private T Create()
        {
            return Factory.Create();
        }

        public void RemoveObjectoFromPool(T obj)
        {
            pool.Push(obj);
            obj.End();
        }
    }
    public abstract class Pool<TParam, TValue> : PoolBase<TValue>, IPool<TParam, TValue> where TValue : IInitialize<TParam, TValue>
    {
        public abstract IFactory<TParam, TValue> Factory { get; set; }
        private Stack<TValue> pool;

        private void OnEnable()
        {
            pool = new Stack<TValue>();
        }

        public TValue AddObjectToPool(TParam param)
        {
            TValue obj = pool.Count > 0 ? pool.Pop() : Create(param);
            obj.Init(param, this);
            return obj;
        }

        private TValue Create(TParam param)
        {
            return Factory.Create(param);
        }

        public void RemoveObjectoFromPool(TValue obj)
        {
            pool.Push(obj);
            obj.End();
        }
    }
    public abstract class Pool<TParam1, TParam2, TValue> : PoolBase<TValue>, IPool<TParam1, TParam2, TValue> where TValue : IInitialize<TParam1, TParam2, TValue>
    {
        public abstract IFactory<TParam1, TParam2, TValue> Factory { get; set; }
        private Stack<TValue> pool;

        private void OnEnable()
        {
            pool = new Stack<TValue>();
        }

        public TValue AddObjectToPool(TParam1 param1, TParam2 param2)
        {
            TValue obj = pool.Count > 0 ? pool.Pop() : Create(param1, param2);
            obj.Init(param1, param2, this);
            return obj;
        }

        private TValue Create(TParam1 param1, TParam2 param2)
        {
            return Factory.Create(param1, param2);
        }

        public void RemoveObjectoFromPool(TValue value)
        {
            pool.Push(value);
            value.End();
        }
    }
}
