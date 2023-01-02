using STGD.Core.Factroy;
using STGD.Core.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    public class SoldierPool : Pool<GameObject, Transform, Vector2Int, Soldier>
    {
        [SerializeField] SoldierFactory factory;
        public override IFactory<GameObject, Transform, Vector2Int, Soldier> Factory { get => factory; set => throw new System.NotImplementedException(); }
    }
}

