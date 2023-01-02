using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace STGD.Core.Pathfinding
{
    public class Node
    {

        public bool walkable;
        public Vector2 WorldPosition { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }

        public int GCost { get; set; }
        public int HCost { get; set; }
        public Node Parent { get; set; }
        public UnitType Type { get; set; }
        
        public int fCost
        {
            get
            {
                return GCost + HCost;
            }
        }
        public Node(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY, UnitType _type)
        {
            walkable = _walkable;
            WorldPosition = _worldPos;
            GridX = _gridX;
            GridY = _gridY;
            Type = _type;
        }

        public Vector2Int GetIntCordinate()
        {
            return new Vector2Int(GridX, GridY);
        }

        public Vector2 GetCordinate()
        {
            return WorldPosition;
        }

        public float GetDistance(Vector2 point1)
        {
            return Vector2.Distance(point1, WorldPosition);
        }

        public enum UnitType
        {
            None,
            Solidier,
            Building
        }
    }
}
