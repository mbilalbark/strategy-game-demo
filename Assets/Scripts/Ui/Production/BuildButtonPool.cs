using UnityEngine;
using STGD.Core.ObjectPooling;
using STGD.Core.Factroy;

namespace STGD.Core.Ui
{
    public class BuildButtonPool : Pool<GameObject, Transform, BuildButton>
    {
        [SerializeField] private BuildButtonFactory factory;
        public override IFactory<GameObject, Transform, BuildButton> Factory { get { return factory; } set { throw new System.NotImplementedException(); } }
    }
}
