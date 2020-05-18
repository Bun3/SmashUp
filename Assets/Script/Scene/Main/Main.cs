using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class Main : MonoBehaviour
    {
        [SerializeField]
        private Text goldText = null;

        private void OnEnable()
        {
            goldText.text = DataManager.GameData.Gold.ToString();
        }

    }

}