using STGD.Core.ObjectPooling;
using UnityEngine;

namespace STGD.Core.Base
{
    [RequireComponent(typeof(BuildingModel))]
    public class Building : Unit, IInitialize<GameObject, Transform, Building>
    {
        private BuildingModel buildingModel;

        public BuildingModel BuildingModel { get => buildingModel; set => buildingModel = value; }

        public void Init(GameObject param1, Transform param2, IPool pool)
        {
            BuildingModel = GetComponent<BuildingModel>();
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

        public void End()
        {
            throw new System.NotImplementedException();
        }

    }
}
