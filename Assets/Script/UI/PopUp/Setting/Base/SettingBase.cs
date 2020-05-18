using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class SettingBase : MonoBehaviour
    {
        protected Image image;
        protected Sprite[] sprites = new Sprite[2];

        private void Awake()
        {
            image = GetComponent<Image>();

            string path = gameObject.name.Replace("]", "");
            sprites[0] = Singleton.game.spriteAtlas.GetSprite("[Setting]" + path + "Off]");
            sprites[1] = Singleton.game.spriteAtlas.GetSprite("[Setting]" + path + "On]");
        }

        public void SetButtonSprite(bool IsOn)
        {
            image.sprite = sprites[System.Convert.ToInt32(IsOn)];
        }
    }

}