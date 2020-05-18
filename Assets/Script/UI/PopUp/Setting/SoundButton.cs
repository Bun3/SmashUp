using UnityEngine.EventSystems;

namespace Zero
{

    public class SoundButton : SettingBase, IPointerClickHandler
    {
        private void OnEnable()
        {
            SetButtonSprite(DataManager.GameData.SoundOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetButtonSprite(DataManager.GameData.SoundOn = !DataManager.GameData.SoundOn);
        }
    }

}