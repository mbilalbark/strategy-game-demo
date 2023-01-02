using STGD.Core.ObjectPooling;
using STGD.Core.PubSub;
using System;
using System.Collections.Generic;
using Grid = STGD.Core.Pathfinding.Grid;
using UnityEngine;
using UnityEngine.UIElements;

namespace STGD.Core.Base
{
    [RequireComponent(typeof(BuildingModel))]
    public class Building : Unit, IInitialize<GameObject, Transform, Building>
    {
        private BuildingModel buildingModel;
        public BuildingModel BuildingModel { get => buildingModel; set => buildingModel = value; }
        public Publisher<Soldier, Building> CreateSoldier;
        public void Init(GameObject param1, Transform param2, IPool pool)
        {
            gameObject.SetActive(true);
            CreateSoldier = new Publisher<Soldier, Building>();
            BuildingModel = GetComponent<BuildingModel>();
            BuildingModel.Pool = pool;
            BuildingModel.Health = BuildingModel.settings.Health;
        }
        public void ChangePosition(Vector2Int position, bool isBuiltable)
        {
            BuildingModel.Position = position;
            if (!isBuiltable)
            {
                BuildingModel.Renderer.color = Color.red;
            }
            else 
            {
                BuildingModel.Renderer.color = Color.green;
            }
        }
        public void Build()
        {
           BuildingModel.Renderer.color = Color.white;
        }
        public void ProductCreate(Soldier soldier)
        {
            CreateSoldier.Publish(soldier, this);
        }
        public Vector2Int GetSpawnSoldier(int circleCount, Grid grid)
        {
            List<Vector2Int> circleGridPoints = new List<Vector2Int>();
            for (int i = 0; i < circleCount; i++)
            {
                for (int x = -i; x < BuildingModel.Width + i + 1; x++)
                {
                    circleGridPoints.Add(new Vector2Int(BuildingModel.Position.x + x, BuildingModel.Position.y - 1 - i));
                    circleGridPoints.Add(new Vector2Int(BuildingModel.Position.x + x, BuildingModel.Position.y + BuildingModel.Height + i));
                }

                for (int y = -i; y < BuildingModel.Height + i + 2; y++)
                {
                    circleGridPoints.Add(new Vector2Int(BuildingModel.Position.x - 1 - i, BuildingModel.Position.y + y - 1));
                    circleGridPoints.Add(new Vector2Int(BuildingModel.Position.x + BuildingModel.Width + i, BuildingModel.Position.y + y - 1));
                }
            }

            Vector2Int spawnPos = new Vector2Int(-1, -1);

            foreach (Vector2Int item in circleGridPoints)
            {
                if(grid.Nodes[item.x, item.y].walkable)
                {
                    grid.Nodes[item.x, item.y].walkable = false;
                    spawnPos = item;
                    break;
                }
                  
            }
            return spawnPos;
        }
        public void End()
        {
            gameObject.SetActive(false);
        }
    }
}
