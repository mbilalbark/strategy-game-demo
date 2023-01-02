using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Singleton;
using STGD.Core.Pathfinding;
using STGD.Core.Settings;
using Grid = STGD.Core.Pathfinding.Grid;
using STGD.Core.PubSub;
using STGD.Core.Base;
using System.Linq;
using JetBrains.Annotations;

namespace STGD.Core.Manager
{
    public class MapCreateManger : Singleton<MapCreateManger>
    {
        [SerializeField] private MapSettings settings;
        [SerializeField] private Transform _mapParent;
        [SerializeField] private BuildingPool buildingPool;
        [SerializeField] private SoldierPool soldierPool;

        private Grid newGrid;
        private Publisher<Vector3> OnClick;
        private Publisher<Unit> OnSelectedUnit;
     
        private List<Building> buildings = new List<Building>();
        private List<Soldier> soldiers = new List<Soldier>();
        private List<Node> path;
        public Grid NewGrid { get => newGrid; set => newGrid = value; }
        public Publisher<Unit> OnDestroyEvent;
        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            Transform mapTile = settings.MapTile;
            int gridSizeX = settings.GridSizeX;
            int gridSizeY = settings.GridSizeY;

            OnDestroyEvent = new Publisher<Unit>();
            OnSelectedUnit = new Publisher<Unit>();
            NewGrid = new Grid(gridSizeX, gridSizeY);
            CreateMap(mapTile, gridSizeX, gridSizeY);
        }

        private List<Transform> nodeList = new List<Transform>();
        private void CreateMap(Transform mapTile, int gridSizeX, int gridSizeY)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Transform newMapTile = Instantiate(mapTile, new Vector2(x, y), Quaternion.identity);
                    nodeList.Add(newMapTile);
                    newMapTile.SetParent(_mapParent);
                }
            }
        }

        public Building CreateBuild(GameObject go)
        {
            Building newBuilding = buildingPool.AddObjectToPool(go, buildingPool.transform);
            Subscriber<Unit>.Subscribe(newBuilding.OnDestroy, OnUnitDestroyed);
            Subscriber<Soldier, Building>.Subscribe(newBuilding.CreateSoldier, OnCreateSoldier);
            buildings.Add(newBuilding);
            return newBuilding;
        }

        private void OnCreateSoldier(Publisher<Soldier, Building> p, Soldier soldier, Building building)
        {
            Vector2Int spawnPos = building.GetSpawnSoldier(3, NewGrid);
            Subscriber<Unit>.Subscribe(soldier.OnDestroy, OnUnitDestroyed);
            Soldier newSoldier = soldierPool.AddObjectToPool(soldier.gameObject, soldierPool.transform, spawnPos);
            soldiers.Add(newSoldier);
        }

        public bool IsNodeBuildable(Vector2Int coordinate, int[] heightWidth)
        {
            for (int i = 0; i < heightWidth[1]; i++)
            {
                for (int j = 0; j < heightWidth[0]; j++)
                {
                    if (NewGrid.Nodes[coordinate.x + i, coordinate.y + j] == null)
                    {
                        return false;
                    }

                    if (NewGrid.Nodes[coordinate.x + i, coordinate.y + j].walkable == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void UpdateGrid(Vector2Int coordinate, int[] heightWidth, Node.UnitType type)
        {
            for (int i = 0; i < heightWidth[1]; i++)
            {
                for (int j = 0; j < heightWidth[0]; j++)
                {
                    NewGrid.Nodes[coordinate.x + i, coordinate.y + j].walkable = false;
                }
            }
        }

        public Unit GetUnitAtPosition(Vector3 position)
        {
           Node node = newGrid.NodeFromWorldPoint(position);
           IEnumerable<Unit> units = GetUnits();

            foreach (var unit in units)
            {
                if (IsUnitContainNode(unit, node))
                {
                    return unit;
                }
            }

            return null;
        }

        private bool IsUnitContainNode(Unit unit, Node node) 
        {
            List<Node> nodes = new List<Node>();
            for (int x = 0; x < unit.UnitModel.Width; x++)
            {
                for (int y = 0; y < unit.UnitModel.Height; y++)
                {
                    nodes.Add(newGrid.Nodes[unit.UnitModel.Position.x + x, unit.UnitModel.Position.y + y]);
                }
            }

            return nodes.Contains(node);
        }

        public Node GetEndMovementNode(Soldier soldier, Unit unit)
        {
            List<Node> circleNodes = unit.GetCircleNodes(NewGrid);
            List<Node> nodes = new List<Node>();

            foreach (Node node in circleNodes)
            {
                Soldier newSoldier  = soldiers.Find(soldier => soldier.SoldierModel.Position == node.GetIntCordinate());

                if (!newSoldier)
                {
                    nodes.Add(node);
                }
            }

            if (nodes.Count == 0)
            {
                return null;
            }

            float minDistance = float.MaxValue;
            int minDistanceNodeIndex = -1;

            for (int i = 0; i < nodes.Count; i++)
            {
                float distance = nodes[i].GetDistance((Vector2)soldier.SoldierModel.Position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    minDistanceNodeIndex = i;
                }
            }

            if (minDistance != -1)
            {
                return nodes[minDistanceNodeIndex];
            }

            return null;
        }

        public List<Node> GetMovementPath(Vector2 firsPoint, Vector2 secondPoint)
        {
            Pathfinder.SetPath(firsPoint, secondPoint, NewGrid);
            path = NewGrid.path;
            return path;
        }

        private IEnumerable<Unit> GetUnits()
        {
            return buildings.Concat<Unit>(soldiers);
        }

        private void OnUnitDestroyed(Publisher<Unit> p, Unit unit)
        {
            Subscriber<Unit>.Unsubscribe(unit.OnDestroy, OnUnitDestroyed);
            GridUnitObjectRemove(unit);
            if (unit is Building building)
            {
                buildings.Remove(building);
            }
            else if (unit is Soldier soldier)
            {
                soldiers.Remove(soldier);
            }

            OnDestroyEvent.Publish(unit);
        }

        private void GridUnitObjectRemove(Unit unit)
        {
            for (int x = 0; x < unit.UnitModel.Width; x++)
            {
                for (int y = 0; y < unit.UnitModel.Height; y++)
                {
                    newGrid.Nodes[unit.UnitModel.Position.x + x, unit.UnitModel.Position.y + y].walkable = true;
                }
            }
        }
    }
}
