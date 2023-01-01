using STGD.Core.Base;
using STGD.Core.Factroy;
using Unity.VisualScripting;
using UnityEngine;

namespace STGD.Core.Ui
{
    public class ProductionButtonFactory : Factory<GameObject, Transform, ProductionButton>
    {
        public override ProductionButton Create(GameObject go, Transform parent)
        {
            return Instantiate(go, parent).GetComponent<ProductionButton>();
        }
    }
}
