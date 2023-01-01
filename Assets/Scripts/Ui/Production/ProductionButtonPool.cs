using UnityEngine;
using STGD.Core.ObjectPooling;
using STGD.Core.Factroy;

namespace STGD.Core.Ui
{
    public class ProductionButtonPool : Pool<GameObject, Transform, ProductionButton>
    {
        [SerializeField] private ProductionButtonFactory factory;
        public override IFactory<GameObject, Transform, ProductionButton> Factory { get { return factory; } set { throw new System.NotImplementedException(); } }
    }
}
