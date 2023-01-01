using STGD.Core.Factroy;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace STGD.Core.Base
{
    public class BuildingFactory : Factory<GameObject, Transform, Building>
    {
        public override Building Create(GameObject go, Transform tr)
        {
            return Instantiate(go, tr).GetComponent<Building>();
        }
    }
}
