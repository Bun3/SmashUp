using UnityEngine.EventSystems;

namespace Zero
{

    public class MusicButton : SettingBase, IPointerClickHandler
    {
        private void OnEnable()
        {
            SetButtonSprite(DataManager.GameData.MusicOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetButtonSprite(DataManager.GameData.MusicOn = !DataManager.GameData.MusicOn);
            Singleton.sound.BgmSource.mute = !DataManager.GameData.MusicOn;
        }
    }

}