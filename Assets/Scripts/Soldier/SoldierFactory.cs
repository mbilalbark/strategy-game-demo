using STGD.Core.Factroy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    public class SoldierFactory : Factory<GameObject, Transform,  Vector2Int, Soldier>
    {
        public override Soldier Create(GameObject go, Transform tr, Vector2Int position)
        {
            Vector3 wordlPosition = new Vector3(position.x, position.y, 0);
            return Instantiate(go, wordlPosition, Quaternion.identity, tr).GetComponent<Soldier>();
        }
    }
}
