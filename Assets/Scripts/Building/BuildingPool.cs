using STGD.Core.Factroy;
using STGD.Core.ObjectPooling;
using UnityEngine;

namespace STGD.Core.Base
{
    public class BuildingPool : Pool<GameObject, Transform, Building>
    {
        [SerializeField] private BuildingFactory factory;
        public override IFactory<GameObject, Transform, Building> Factory { get { return factory; } set => throw new System.NotImplementedException(); }
    }
}
