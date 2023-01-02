using STGD.Core.Base;
using UnityEngine;
using UnityEngine.UI;

namespace STGD.Core.Ui
{
    public class ProductButton : MonoBehaviour
    {
        private Image image;
        private Button button;
        private Unit unit;
        private Soldier soldier;

        public void Init(Sprite sprite, Unit unit, Soldier product)
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
            image.sprite = sprite;
            this.unit = unit;
            this.soldier = product;
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (unit is Building building)
            {
                building.ProductCreate(soldier);
            }
        }
    }
}
