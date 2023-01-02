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

            if (node.GridX > 0)
                neighbours.Add(Nodes[node.GridX - 1, node.GridY]);

            if (node.GridX < gridSizeX - 1) 
                neighbours.Add(Nodes[node.GridX + 1, node.GridY]);
            
            if (node.GridY > 0) 
                neighbours.Add(Nodes[node.GridX, node.GridY - 1]);

            if (node.GridY < gridSizeY - 1) 
                neighbours.Add(Nodes[node.GridX, node.GridY + 1]);
 
            return neighbours;
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x);
            int y = Mathf.FloorToInt(worldPosition.y);
            if (x >= gridSizeX || y >= gridSizeY || x < 0 || y < 0)
            {
                return null;
            }
            return Nodes[x, y];
        }
    }
}
