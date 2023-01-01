using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.Singleton;
using UnityEngine.EventSystems;
using STGD.Core.PubSub;

namespace STGD.Core.Manager
{
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] private EventSystem current;
        [SerializeField] Camera _camera;
        [SerializeField] private Vector2 boundMax, boudnMin;
        private bool isArea;
        private bool ýsClick;
        private Vector3 mousePosition;
        private Vector3 lastMouseButtonPosition;

        public Publisher<Vector3> OnClick;

        public bool IsArea { get => isArea; set => isArea = value; }
        public Vector3 MousePosition { get => mousePosition; }
        public Vector3 LastMouseButtonPosition { get => lastMouseButtonPosition; set => lastMouseButtonPosition = value; }
        public bool IsClick { get => ýsClick; set => ýsClick = value; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void Init()
        {
            OnClick = new Publisher<Vector3>();
        }

        private void Update()
        {
            mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (EventCheck())
            {
                if (ControleMouseArea(MousePosition))
                    IsArea = true;
                else
                    IsArea = false;

                if (Input.GetMouseButtonDown(0))
                {
                    LastMouseButtonPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    OnClick.Publish(LastMouseButtonPosition);
                }
            }
        }

        public bool EventCheck()
        {
            return (current.currentSelectedGameObject == null || (current.currentSelectedGameObject != null && current.currentSelectedGameObject.layer != 5));
        }

        private bool ControleMouseArea(Vector3 mouseScreenPos)
        {
            if (mouseScreenPos.x < boundMax.x && mouseScreenPos.x > boudnMin.x && mouseScreenPos.y < boundMax.y && mouseScreenPos.y > boudnMin.y)
            {
                return true;
            }
            return false;
        }

    }
}
