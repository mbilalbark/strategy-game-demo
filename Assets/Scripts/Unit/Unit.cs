using UnityEngine;
using STGD.Core.ObjectPooling;
using STGD.Core.Pathfinding;
using Grid = STGD.Core.Pathfinding.Grid;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using STGD.Core.PubSub;

namespace STGD.Core.Base
{
    [RequireComponent(typeof(UnitModel))]
    public class Unit : MonoBehaviour
    {
        private UnitModel unitModel;

        public UnitModel UnitModel { get => unitModel; set => unitModel = value; }
        public Publisher<Unit> OnDestroy = new Publisher<Unit>();
        private void Awake()
        {
            UnitModel = GetComponent<UnitModel>();
        }

        public void Damage(int damage)
        {
            UnitModel.Health -= damage;

            StartCoroutine(ChangeColor());
            
            if (UnitModel.Health <= 0)
            {
                UnitModel.Pool.RemoveObjectoFromPool(this);
                OnDestroy.Publish(this);
            }
        }

        private IEnumerator ChangeColor()
        {
            UnitModel.Renderer.color = Color.red;
            yield return new WaitForSeconds(.2f);
            UnitModel.Renderer.color = Color.white;
        }

        public List<Node> GetCircleNodes(Grid grid)
        {
            List<Node> nodes = new List<Node>();

            for (int x = -1; x < UnitModel.Width + 1; x++)
            {
                nodes.Add(grid.Nodes[UnitModel.Position.x + x, UnitModel.Position.y - 1]);
                nodes.Add(grid.Nodes[UnitModel.Position.x + x, UnitModel.Position.y + UnitModel.Height]);
            }

            for (int y = 0; y < UnitModel.Height + 2; y++)
            {
                nodes.Add(grid.Nodes[UnitModel.Position.x - 1, UnitModel.Position.y + y - 1]);
                nodes.Add(grid.Nodes[UnitModel.Position.x + UnitModel.Width, UnitModel.Position.y + y - 1]);
            }

            return nodes;
        }
    }
}
 