using STGD.Core.Factroy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    public class SolidierFactory : Factory<Soldier>
    {
        [SerializeField] private Soldier prefab;
        public override Soldier Create()
        {
            Soldier soldier = Instantiate(prefab);
            return soldier;
        }
    }
}
