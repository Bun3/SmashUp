using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class SkinShop : MonoBehaviour
    {
        private Image selectImage;
        private Text selectText;

        private Image[] buttons;
        private RectTransform[] buttonRects;
        private Vector2[] deltaSizes;

        private int SkinIndex;

        [SerializeField]
        private Text goldText = null;

        private void Awake()
        {
            GameObject select = transform.Find("[Select]").gameObject;
            selectImage = select.GetComponent<Image>();
            selectText = select.transform.GetChild(0).GetComponent<Text>();

            buttons = new Image[2];
            buttonRects = new RectTransform[2];
            deltaSizes = new Vector2[DataManager.define_AllSkinCnt];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = transform.GetChild(transform.childCount - (buttons.Length - i)).GetComponent<Image>();
                buttonRects[i] = transform.GetChild(transform.childCount - (buttons.Length - i)).GetComponent<RectTransform>();
            }

            deltaSizes = new Vector2[] { new Vector2(128, 128), new Vector2(128, 128), new Vector2(128, 128), new Vector2(128, 128), new Vector2(128, 160) };
        }

        private void OnEnable()
        {
            goldText.text = DataManager.GameData.Gold.ToString();
            SkinIndex = DataManager.GameData.SkinIndex;

            SetButtonSprite(SkinIndex);
            CheckPurchase();
        }

        public void Next()
        {
            if (SkinIndex.Equals(DataManager.define_AllSkinCnt))
                SkinIndex = 1;
            else
                SkinIndex++;

            SetButtonSprite(SkinIndex);
            CheckPurchase();
        }

        public void Prev()
        {
            if (SkinIndex.Equals(1))
                SkinIndex = DataManager.define_AllSkinCnt;
            else
                SkinIndex--;

            SetButtonSprite(SkinIndex);
            CheckPurchase();
        }

        public void ChooseThisSkin()
        {
            if (DataManager.GameData.PurChases[1][SkinIndex - 1].IsCollected)
            {
                Singleton.scene.ChangeScene(Scene.Main);
                DataManager.GameData.SkinIndex = SkinIndex;
            }
            else if (DataManager.GameData.PurChases[1][SkinIndex - 1].Need <= DataManager.GameData.Gold)
            {
                DataManager.GameData.PurChases[1][SkinIndex - 1].IsCollected = true;
                DataManager.GameData.Gold -= DataManager.GameData.PurChases[1][SkinIndex - 1].Need;
                goldText.text = DataManager.GameData.Gold.ToString();
                CheckPurchase();
            }
            else
            {
                //광고 팝업을 띄우던가 뭔가를 하던가?
            }
        }

        private void CheckPurchase()
        {
            if (DataManager.GameData.PurChases[1][SkinIndex - 1].IsCollected)
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Select]");
                selectText.text = "SELECT";
            }
            else if (DataManager.GameData.PurChases[1][SkinIndex - 1].Need <= DataManager.GameData.Gold)
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][UnLock]");
                selectText.text = DataManager.GameData.PurChases[1][SkinIndex - 1].Need.ToString();
            }
            else
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Lock]");
                selectText.text = DataManager.GameData.PurChases[1][SkinIndex - 1].Need.ToString();
            }
        }

        private void SetButtonSprite(int index)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                string atlasPath = "[Button][" + index.ToString() + "][" + (i + 1).ToString() + "]";
                buttons[i].sprite = Singleton.game.spriteAtlas.GetSprite(atlasPath);
                buttonRects[i].sizeDelta = deltaSizes[index - 1] * 2;
            }
        }
    }

}