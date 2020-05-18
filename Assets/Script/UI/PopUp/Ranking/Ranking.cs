using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class Ranking : MonoBehaviour
    {
        [SerializeField]
        private Image portrait = null;
        [SerializeField]
        private Text bestText = null;
        [SerializeField]
        private Text totalText = null;

        private void OnEnable()
        {
            portrait.sprite = Resources.Load<Sprite>("Sprite/GameObject/Character/Character_" +
                DataManager.GameData.BestScoreCharIndex.ToString() + "/Idle/[Idle][1]");
            portrait.rectTransform.sizeDelta
                = new Vector2(portrait.sprite.texture.width * 4, portrait.sprite.texture.height * 4);

            bestText.text = DataManager.GameData.CharacterBestScore[DataManager.GameData.BestScoreCharIndex - 1].ToString();
            totalText.text = DataManager.GameData.TotalScore.ToString();
        }

    }

}