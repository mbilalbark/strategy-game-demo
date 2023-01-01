using UnityEngine;
using STGD.Core.Singleton;

namespace STGD.Core.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            InputManager.Instance.Init();
            MapCreateManger.Instance.Init();
            UiManager.Instance.Init();
            LevelManager.Instance.Init();
        }
    }
}
