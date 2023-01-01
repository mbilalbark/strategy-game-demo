using STGD.Core.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.ObjectPooling
{
    public interface IInitialize
    {

    }

    public interface IInitialize<T>: IInitialize
    {
        void Init(IPool pool);
        void End();
    }

    public interface IInitialize<TParam, TValue>: IInitialize
    {
        void Init(TParam param, IPool pool);
        void End();
    }

    public interface IInitialize<TParam1, TParam2, TValue>: IInitialize
    {
        void Init(TParam1 param1, TParam2 param2, IPool pool);
        void End();
    }
}
