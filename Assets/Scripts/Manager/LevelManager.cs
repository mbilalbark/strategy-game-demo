using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Base;
using STGD.Core.Singleton;
using STGD.Core.PubSub;
using STGD.Core.Pathfinding;
using System.IO;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.PackageManager;

namespace STGD.Core.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        private Building building;
        private Unit selectedUnit;
        private Publisher<Vector3> OnClick;

        public Publisher<Unit> OnSelect;
        

        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            OnClick = InputManager.Instance.OnClick;
            OnSelect = new Publisher<Unit>();
            Subscriber<Unit>.Subscribe(MapCreateManger.Instance.OnDestroyEvent, OnUnitDestroyed);
            Subscriber<Vector3>.Subscribe(OnClick, OnClicked);
        }

        private void OnUnitDestroyed(Publisher<Unit> p, Unit entity)
        {
            OnSelect.Publish(null);
        }
        public void OnClickBuildButton(Publisher<BuildButtonModel> p, BuildButtonModel e)
        {
            if (building)
                return;

            building = MapCreateManger.Instance.CreateBuild(e.Building.gameObject);
        }      

        private void OnClicked(Publisher<Vector3> p, Vector3 inputPos) 
        {
            if (building)
            {
                Node node = MapCreateManger.Instance.NewGrid.NodeFromWorldPoint(inputPos);

                if (node != null)
                {
                    Vector2Int nodeCoordinate = node.GetIntCordinate();
                    int[] heightWidth = new int[2];
                    heightWidth[0] = building.BuildingModel.Height;
                    heightWidth[1] = building.BuildingModel.Width;
                    bool isBuild = MapCreateManger.Instance.IsNodeBuildable(nodeCoordinate, heightWidth);
                    if (isBuild)
                    {
                        building.Build();
                        MapCreateManger.Instance.UpdateGrid(nodeCoordinate, heightWidth, Node.UnitType.Building);
                        building = null;
                    }
                }
                return;
            }

            var unit = MapCreateManger.Instance.GetUnitAtPosition(inputPos);
            if (unit)
            {
                OnSelect.Publish(unit);
            }
            else
            {
                OnSelect.Publish(null);
            }
            
            if (selectedUnit)
            {
                if (selectedUnit is Soldier soldier)
                {
                    Node node = MapCreateManger.Instance.NewGrid.NodeFromWorldPoint(inputPos);
                    if (unit && unit != selectedUnit)
                    {
                        Node endMovementNode = MapCreateManger.Instance.GetEndMovementNode(soldier, unit);
                        if (endMovementNode != null)
                        {
                            List<Node> path = MapCreateManger.Instance.GetMovementPath(soldier.SoldierModel.Position, endMovementNode.GetCordinate());
                            soldier.SetPath(path, false, true, unit);
                        }
                    }
                    else
                    {
                        List<Node> path = MapCreateManger.Instance.GetMovementPath(soldier.SoldierModel.Position, node.GetCordinate());
                        soldier.SetPath(path, true, false);
                    }
                }
            }
            selectedUnit = unit;
        }

        private void Update()
        {
            if (building)
            {
                Node node = MapCreateManger.Instance.NewGrid.NodeFromWorldPoint(InputManager.Instance.MousePosition);
                if (node != null)
                {
                    Vector2Int nodeCoordinate = node.GetIntCordinate();
                    int[] heightWidth = new int[2];
                    heightWidth[0] = building.BuildingModel.Height;
                    heightWidth[1] = building.BuildingModel.Width;
                    building.ChangePosition(nodeCoordinate, MapCreateManger.Instance.IsNodeBuildable(nodeCoordinate, heightWidth));
                }
            }
        }

        private void OnDestroy()
        {
            Subscriber<Vector3>.Unsubscribe(OnClick, OnClicked);
        }
    }
}
