using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class LandShop : MonoBehaviour
    {
        private SpriteRenderer landSpriter;
        private Image selectImage;
        private Text selectText;

        private Sprite[] lands;

        private int LandIndex;

        [SerializeField]
        private Text goldText = null;

        private void Awake()
        {
            GameObject select = transform.Find("[Select]").gameObject;
            selectImage = select.GetComponent<Image>();
            selectText = select.transform.GetChild(0).GetComponent<Text>();

            landSpriter = Singleton.game.backGround.GetComponent<SpriteRenderer>();

            lands = new Sprite[DataManager.define_AllLandCnt];
            for (int i = 1; i <= DataManager.define_AllLandCnt; i++)
                lands[i - 1] = Resources.Load<Sprite>("Sprite/GameObject/BackGround/[" + i.ToString() + "]");
        }

        private void OnEnable()
        {
            goldText.text = DataManager.GameData.Gold.ToString();
            LandIndex = DataManager.GameData.LandIndex;

            landSpriter.sprite = lands[LandIndex];
            CheckPurchase();
        }

        public void Next()
        {
            if (LandIndex.Equals(DataManager.define_AllLandCnt - 1))
                LandIndex = 0;
            else
                LandIndex++;

            landSpriter.sprite = lands[LandIndex];
            CheckPurchase();
        }

        public void Prev()
        {
            if (LandIndex.Equals(0))
                LandIndex = DataManager.define_AllLandCnt - 1;
            else
                LandIndex--;

            landSpriter.sprite = lands[LandIndex];
            CheckPurchase();
        }

        public void ChooseThisLand()
        {
            if (DataManager.GameData.PurChases[2][LandIndex].IsCollected)
            {
                Singleton.scene.ChangeScene(Scene.Main);
                DataManager.GameData.LandIndex = LandIndex;
            }
            else if (DataManager.GameData.PurChases[2][LandIndex].Need <= DataManager.GameData.Gold)
            {
                DataManager.GameData.PurChases[2][LandIndex].IsCollected = true;
                DataManager.GameData.Gold -= DataManager.GameData.PurChases[0][LandIndex].Need;
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
            if (DataManager.GameData.PurChases[2][LandIndex].IsCollected)
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Select]");
                selectText.text = "SELECT";
            }
            else if (DataManager.GameData.PurChases[2][LandIndex].Need <= DataManager.GameData.Gold)
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][UnLock]");
                selectText.text = DataManager.GameData.PurChases[2][LandIndex].Need.ToString();
            }
            else
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Lock]");
                selectText.text = DataManager.GameData.PurChases[2][LandIndex].Need.ToString();
            }
        }
    }

};