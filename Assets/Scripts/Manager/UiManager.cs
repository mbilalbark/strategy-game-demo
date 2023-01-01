using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Singleton;
using STGD.Core.Ui;

namespace STGD.Core.Manager
{
    public class UiManager : Singleton<UiManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            ProductionUi.Instance.Init();
        }
    }
}
