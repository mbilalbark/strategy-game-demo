using STGD.Core.Base;
using STGD.Core.Manager;
using STGD.Core.ObjectPooling;
using STGD.Core.PubSub;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace STGD.Core.Ui
{
    [RequireComponent(typeof( ProductionButtonModel))]
    public class ProductionButton : MonoBehaviour, IInitialize<GameObject, Transform, ProductionButton>
    {
        private Button button;
        private ProductionButtonModel productionButtonModel;
        public Publisher<ProductionButtonModel> newPublisher;
        
        public void Init(GameObject go, Transform tr, IPool pool)
        {
            button = GetComponent<Button>();
            productionButtonModel = GetComponent<ProductionButtonModel>();
            newPublisher = new Publisher<ProductionButtonModel>();
            button.onClick.AddListener(OnClick);
            Subscriber<ProductionButtonModel>.Subscribe(newPublisher, LevelManager.Instance.OnClickProductionButton);
        }

        private void OnClick()
        {
            newPublisher.Publish(productionButtonModel);
        }

        public void End()
        {
            button.onClick.RemoveListener(OnClick);
            Subscriber<ProductionButtonModel>.Unsubscribe(newPublisher, LevelManager.Instance.OnClickProductionButton);
        }
    }
}
