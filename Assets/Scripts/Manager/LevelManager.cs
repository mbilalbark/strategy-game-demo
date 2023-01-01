using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Base;
using STGD.Core.Singleton;
using STGD.Core.Ui;
using STGD.Core.PubSub;

namespace STGD.Core.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {

        }
        public void OnClickProductionButton(Publisher<ProductionButtonModel> p, ProductionButtonModel e)
        {
            MapCreateManger.Instance.CreateBuild(e.Building.gameObject);
        }

        public void OnClickInformationPanel()
        {
            // Handle
        }
    }
}
