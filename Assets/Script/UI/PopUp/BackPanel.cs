using UnityEngine.EventSystems;
using UnityEngine;

namespace Zero
{
    public class BackPanel : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }
    }

}