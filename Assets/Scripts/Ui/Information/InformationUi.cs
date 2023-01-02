using STGD.Core.PubSub;
using STGD.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Base;
using TMPro;
using UnityEngine.UI;
using STGD.Core.Manager;
using static UnityEngine.EventSystems.EventTrigger;

namespace STGD.Core.Ui
{
    public class InformationUi : Singleton<InformationUi>
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image view;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject detailContent;
        [SerializeField] private List<ProductButton> productButtons;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            PanelSetActivate(false);
            Subscriber<Unit>.Subscribe(LevelManager.Instance.OnSelect, OpenInfoPanel);
        }

        private void OpenInfoPanel(Publisher<Unit> p, Unit unit)
        {
            if (unit == null)
            {
                PanelSetActivate(false);
                return;
            }

            PanelSetActivate(true);
            label.text = unit.UnitModel.Title;
            view.sprite = unit.UnitModel.Sprite;
           
            if (unit is Building building)
            {
                detailContent.SetActive(true);
                List<Product> products = building.BuildingModel.Products;
                
                if(products.Count > 0)
                    detailContent.SetActive(true);
                else
                    detailContent.SetActive(false);

                for (int i = 0; i < products.Count; i++)
                {
                    productButtons[i].Init(products[i].ProductModel.Sprite, unit, products[i].ProductModel.Soldier);
                }
            }
            else
                detailContent.SetActive(false);
        }

        private void PanelSetActivate(bool isActive)
        {
            infoPanel.SetActive(isActive);
        }
    }
}
