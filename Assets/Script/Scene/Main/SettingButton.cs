using UnityEngine;
using UnityEngine.EventSystems;

namespace Zero
{

    public class SettingButton : MonoBehaviour, IPointerClickHandler
    {
        GameObject settingPanel = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (settingPanel == null)
            {
                GameObject setting = (Resources.Load<GameObject>("Prefabs/PopUp/[Setting]"));
                settingPanel = Instantiate(setting, GameObject.Find("[PopUpUIZone]").transform);
            }
            else
                settingPanel.SetActive(true);
        }
    }
}