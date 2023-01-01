using STGD.Core.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    public class UnitModel : MonoBehaviour
    {
        protected IPool pool;
        [SerializeField] protected int width;
        public int Width { get { return width; } set { width = value; } }

        [SerializeField] protected int height;
        public int Height { get { return height; } }
        [SerializeField] protected int health;
        public int Health { get { return health; } }
        protected Vector2Int position;
        public Vector2Int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;

                transform.position = new Vector3(position.x, position.y, 0);
            }
        }


    }
}
