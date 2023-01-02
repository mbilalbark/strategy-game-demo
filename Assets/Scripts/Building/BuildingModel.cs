using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace STGD.Core.Base
{
    public class BuildingModel : UnitModel
    {
        [SerializeField]
        private List<Product> products;
        public List<Product> Products { get => products; set => products = value; }
    }
}
