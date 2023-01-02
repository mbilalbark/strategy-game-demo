using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    [RequireComponent(typeof(ProductModel))]
    public class Product : Unit
    {
        [SerializeField]
        private ProductModel productModel;

        public ProductModel ProductModel { get => productModel; set => productModel = value; }

    }
}
