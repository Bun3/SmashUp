using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Zero
{
    public enum DIRECTION
    {
        DIRECTION_LEFT = 1 , DIRECTION_RIGHT, DIRECTION_NOTHING
    };

    public class InputButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private DIRECTION dir = DIRECTION.DIRECTION_NOTHING;
        [SerializeField]
        private RectTransform targetButton = null;

        private InGame parent;

        [Range(0.8f, 0.9f)]
        float scopeVal = 0.9f;

        private void Awake()
        {
            parent = transform.parent.GetComponent<InGame>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Singleton.command.IsInputTime)
            {
                Singleton.command.InputQ.Enqueue((int)dir);

                if (Singleton.command.CompareNowInput())
                    StartCoroutine(parent.IInputSuccess());
                else
                    StartCoroutine(parent.IGameOver());

                if (Singleton.command.CompareNowPattern())
                    StartCoroutine(parent.IInputAllSuccess());
            }
        }

        public void Up() => targetButton.localScale = Vector3.one;
        public void Down() => targetButton.localScale = Vector3.one * scopeVal;

        public void OnPointerUp(PointerEventData eventData) => Up();

        public void OnPointerDown(PointerEventData eventData) => Down();
    }

}