using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace STGD.Core.Base
{
    public class ProductModel : UnitModel
    {
        [SerializeField]
        private Soldier soldier;
        public Soldier Soldier { get => soldier; set => soldier = value; }
    }
}
