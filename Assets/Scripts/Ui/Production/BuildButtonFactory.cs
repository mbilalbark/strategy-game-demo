using STGD.Core.Base;
using STGD.Core.Factroy;
using Unity.VisualScripting;
using UnityEngine;

namespace STGD.Core.Ui
{
    public class BuildButtonFactory : Factory<GameObject, Transform, BuildButton>
    {
        public override BuildButton Create(GameObject go, Transform parent)
        {
            return Instantiate(go, parent).GetComponent<BuildButton>();
        }
    }
}
