using STGD.Core.Base;
using STGD.Core.Manager;
using STGD.Core.ObjectPooling;
using STGD.Core.PubSub;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace STGD.Core.Ui
{
    [RequireComponent(typeof( BuildButtonModel))]
    public class BuildButton : MonoBehaviour, IInitialize<GameObject, Transform, BuildButton>
    {
        private Button button;
        private BuildButtonModel buildButtonModel;
        public Publisher<BuildButtonModel> newPublisher;
        
        public void Init(GameObject go, Transform tr, IPool pool)
        {
            button = GetComponent<Button>();
            buildButtonModel = GetComponent<BuildButtonModel>();
            newPublisher = new Publisher<BuildButtonModel>();
            button.onClick.AddListener(OnClick);
            Subscriber<BuildButtonModel>.Subscribe(newPublisher, LevelManager.Instance.OnClickBuildButton);
        }

        private void OnClick()
        {
            newPublisher.Publish(buildButtonModel);
        }

        public void End()
        {
            button.onClick.RemoveListener(OnClick);
            Subscriber<BuildButtonModel>.Unsubscribe(newPublisher, LevelManager.Instance.OnClickBuildButton);
        }
    }
}
