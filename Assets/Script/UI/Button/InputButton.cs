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

    public class InputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

        public void Up() => targetButton.localScale = Vector3.one;
        public void Down()
        {
            if (Singleton.command.IsInputTime)
            {
                Singleton.command.InputQ.Enqueue((int)dir);

                StartCoroutine(Singleton.command.CompareNowInput() ? parent.IInputSuccess() : parent.IGameOver());

                if (Singleton.command.CompareNowPattern())
                    StartCoroutine(parent.IInputAllSuccess());
            }

            targetButton.localScale = Vector3.one * scopeVal;
        }
        public void OnPointerUp(PointerEventData eventData) => Up();

        public void OnPointerDown(PointerEventData eventData) => Down();
    }

}