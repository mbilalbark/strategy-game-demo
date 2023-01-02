using STGD.Core.ObjectPooling;
using UnityEngine;
using STGD.Core.Settings;

namespace STGD.Core.Base
{
    public class UnitModel : MonoBehaviour
    {
        public UnitSettings settings;
        protected IPool pool;
        public IPool Pool { get => pool; set => pool = value; }
        [SerializeField] protected int width;
        public int Width { get { return width; } }

        [SerializeField] protected int height;
        public int Height { get { return height; } }
        [SerializeField] protected int health;
        public int Health { get { return health; } set { health = value; } }

        [SerializeField] protected Sprite sprite;
        public Sprite Sprite { get { return sprite; } }

        [SerializeField] protected string title;
        public string Title { get { return title; } }

        [SerializeField] protected SpriteRenderer renderer;
        public SpriteRenderer Renderer { get { return renderer; } set { renderer = value; } }

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
        }    }
}
