using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Singleton;
using STGD.Core.Pathfinding;
using STGD.Core.Settings;
using Grid = STGD.Core.Pathfinding.Grid;
using STGD.Core.PubSub;
using STGD.Core.Base;
using STGD.Core.ObjectPooling;

namespace STGD.Core.Manager
{
    public class MapCreateManger : Singleton<MapCreateManger>
    {
        [SerializeField] private MapSettings settings;
        [SerializeField] private Transform _mapParent;
      

        [SerializeField]
        private BuildingPool buildingPool;
        private Building building;
        private Grid newGrid;
        private Publisher<Vector3> OnClick;
        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            Transform mapTile = settings.MapTile;
            int gridSizeX = settings.GridSizeX;
            int gridSizeY = settings.GridSizeY;
            OnClick = InputManager.Instance.OnClick;
            Subscriber<Vector3>.Subscribe(OnClick, OnClicked);
            newGrid = new Grid(gridSizeX, gridSizeY);
            CreateMap(mapTile, gridSizeX, gridSizeY);
        }
        private void CreateMap(Transform mapTile, int gridSizeX, int gridSizeY)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Transform newMapTile = Instantiate(mapTile, new Vector2(x, y), Quaternion.identity);
                    newMapTile.SetParent(_mapParent);
                }
            }
        }

        private void OnClicked(Publisher<Vector3> p, Vector3 e)
        {
            if (building)
            {
                Node node = newGrid.NodeFromWorldPoint(e);

                if (node != null)
                {
                    Vector2Int nodeCoordinate = node.GetIntCordinate();
                    int[] heightWidth = new int[2];
                    heightWidth[0] = building.BuildingModel.Height;
                    heightWidth[1] = building.BuildingModel.Width;
                    bool isBuild = IsNodeBuildable(nodeCoordinate, heightWidth);
                    if(isBuild)
                    {
                        building.Build();
                        UpdateGrid(nodeCoordinate, heightWidth);
                        building = null;
                    }
                }
            }
        }
        public void CreateBuild(GameObject go)
        {
            building = buildingPool.AddObjectToPool(go, buildingPool.transform);
        }

        private void Update()
        {
            if (building)
            {
                Node node = newGrid.NodeFromWorldPoint(InputManager.Instance.MousePosition);
                if (node != null)
                {
                    Vector2Int nodeCoordinate = node.GetIntCordinate();
                    int[] heightWidth = new int[2];
                    heightWidth[0] = building.BuildingModel.Height;
                    heightWidth[1] = building.BuildingModel.Width;
                    building.ChangePosition(nodeCoordinate, IsNodeBuildable(nodeCoordinate, heightWidth));
                }
            }
        }

        private bool IsNodeBuildable(Vector2Int coordinate, int[] heightWidth)
        {
            for (int i = 0; i < heightWidth[1]; i++)
            {
                for (int j = 0; j < heightWidth[0]; j++)
                {
                    if (newGrid.Nodes[coordinate.x + i, coordinate.y + j] == null)
                    {
                        return false;
                    }

                    if (newGrid.Nodes[coordinate.x + i, coordinate.y + j].walkable == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void UpdateGrid(Vector2Int coordinate, int[] heightWidth)
        {
            for (int i = 0; i < heightWidth[1]; i++)
            {
                for (int j = 0; j < heightWidth[0]; j++)
                {
                    newGrid.Nodes[coordinate.x + i, coordinate.y + j].walkable = false;
                }
            }
        }
    }
}
