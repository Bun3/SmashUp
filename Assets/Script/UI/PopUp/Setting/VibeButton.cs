using UnityEngine.EventSystems;

namespace Zero
{

    public class VibeButton : SettingBase, IPointerClickHandler
    {
        private void OnEnable()
        {
            SetButtonSprite(DataManager.GameData.VibeOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetButtonSprite(DataManager.GameData.VibeOn = !DataManager.GameData.VibeOn);
        }
    }

}