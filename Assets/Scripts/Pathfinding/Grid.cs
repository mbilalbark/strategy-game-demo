using UnityEngine;
using System.Collections.Generic;


namespace STGD.Core.Pathfinding
{
    public class Grid 
    {

        public LayerMask unwalkableMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        public List<Node> path;

        private Node[,] nodes;
        private float nodeDiameter;
        private int gridSizeX, gridSizeY;



        public Grid(int gridSizeX, int gridSizeY)
        {
            this.gridSizeX = gridSizeX;
            this.gridSizeY = gridSizeY;
            CreateGrid();
        }

        public Node[,] Nodes { get => nodes; set => nodes = value; }

        public void CreateGrid()
        {
            Nodes = new Node[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Nodes[x, y] = new Node(true, new Vector2(x, y), x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(Nodes[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }


        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x);
            int y = Mathf.FloorToInt(worldPosition.y);
            //float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            //float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
            //percentX = Mathf.Clamp01(percentX);
            //percentY = Mathf.Clamp01(percentY);

            //int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            //int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
            if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            {
                return null;
            }
            return Nodes[x, y];
        }

        
        //void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        //    if (grid != null)
        //    {
        //        foreach (Node n in grid)
        //        {
        //            Gizmos.color = (n.walkable) ? Color.white : Color.red;
        //            if (path != null)
        //                if (path.Contains(n))
        //                    Gizmos.color = Color.black;
        //            Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - .1f));
        //        }
        //    }
        //}
    }
}
