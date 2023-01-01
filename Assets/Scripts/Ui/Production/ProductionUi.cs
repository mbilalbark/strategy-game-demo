using STGD.Core.ObjectPooling;
using STGD.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Ui
{
    public class ProductionUi : Singleton<ProductionUi>
    {
        [SerializeField] private int productionCount;
        [SerializeField] private GameObject[] productions;
        [SerializeField] private Transform content;
        [SerializeField] private Pool<GameObject, Transform, ProductionButton> productionPool;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            for (int i = 0; i < productionCount; i++)
            {
                for (int j = 0; j < productions.Length; j++)
                {
                    productionPool.AddObjectToPool(productions[j], content);
                }
            }
        }
    }
}
