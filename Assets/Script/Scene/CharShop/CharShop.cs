using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class CharShop : MonoBehaviour
    {
        private Image selectImage;
        private Text selectText;

        private int CharIndex;

        [SerializeField]
        private Text goldText = null;

        private void Awake()
        {
            GameObject select = transform.Find("[Select]").gameObject;
            selectImage = select.GetComponent<Image>();
            selectText = select.transform.GetChild(0).GetComponent<Text>();

        }

        private void OnEnable()
        {
            Camera.main.transform.position = new Vector3(0, -2, -10);
            Camera.main.orthographicSize = 3;

            goldText.text = DataManager.GameData.Gold.ToString();

            CharIndex = DataManager.GameData.CharIndex;
            Singleton.game.character.SetCharacter(CharIndex);
            CheckPurchase();
        }

        private void OnDisable()
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
            Camera.main.orthographicSize = 5;

        }

        public void Next()
        {
            if (CharIndex.Equals(DataManager.define_AllCharCnt))
                CharIndex = 1;
            else
                CharIndex++;

            Singleton.game.character.SetCharacter(CharIndex);
            CheckPurchase();
        }

        public void Prev()
        {
            if (CharIndex.Equals(1))
                CharIndex = DataManager.define_AllCharCnt;
            else
                CharIndex--;

            Singleton.game.character.SetCharacter(CharIndex);
            CheckPurchase();
        }

        public void ChooseThisCharacter()
        {
            if (DataManager.GameData.PurChases[0][CharIndex - 1].IsCollected)
            {
                Singleton.scene.ChangeScene(Scene.Main);
                DataManager.GameData.CharIndex = CharIndex;
            }
            else if (DataManager.GameData.PurChases[0][CharIndex - 1].Need <= DataManager.GameData.Gold)
            {
                DataManager.GameData.PurChases[0][CharIndex - 1].IsCollected = true;
                DataManager.GameData.Gold -= DataManager.GameData.PurChases[0][CharIndex - 1].Need;
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
            if (DataManager.GameData.PurChases[0][CharIndex - 1].IsCollected)
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Select]");
                selectText.text = "SELECT";
            }
            else if (DataManager.GameData.PurChases[0][CharIndex - 1].Need <= DataManager.GameData.Gold) 
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][UnLock]");
                selectText.text =  DataManager.GameData.PurChases[0][CharIndex-1].Need.ToString();
            }
            else
            {
                selectImage.sprite = Singleton.game.spriteAtlas.GetSprite("[Shop][Lock]");
                selectText.text =  DataManager.GameData.PurChases[0][CharIndex-1].Need.ToString();
            }
        }
    }

}