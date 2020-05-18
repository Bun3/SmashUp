using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        RectTransform rt;

        [Range(0.8f, 0.9f)]
        float scopeVal = 0.9f;

        private void Awake()
        {
            rt = GetComponent<RectTransform>();
        }

        public void ButtonUp()
        {
            rt.localScale = Vector3.one;
        }

        public void ButtonDown()
        {
            rt.localScale = Vector3.one * scopeVal;
            Singleton.sound.Play("Click");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonUp();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonDown();
        }
    }

};