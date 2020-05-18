using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Zero
{

    public class DoubleCoin : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField]
        private Text coinText = null;

        private bool isDone = false;

        public void OnPointerClick(PointerEventData eventData) { print("Click"); if (isDone) Destroy(gameObject); }

        public void CoinCountUp(int before, int after) => StartCoroutine(ICoinCountUp(before, after));
        private IEnumerator ICoinCountUp(int beforeCoin, int afterCoin)
        {
            int coin = beforeCoin;
            int totalUpCoin = afterCoin - beforeCoin;

            coinText.text = coin.ToString();

            if (totalUpCoin <= 0) { isDone = true; Destroy(gameObject); yield break; }

            int coinLength = Mathf.RoundToInt(Mathf.Log10(totalUpCoin)) + 1;
            int coinUpUnit = coinLength >= 3 ? coinLength : 1;
            coinUpUnit = coinLength >= 4 ? 2 * Mathf.RoundToInt(Mathf.Pow(10, coinLength - 3)) : coinUpUnit;

            yield return new WaitForSeconds(1f);

            while (coin <= afterCoin)
            {
                coinText.text = coin.ToString();
                yield return null;
                coin += coinUpUnit;
            }

            coinText.text = afterCoin.ToString();
            isDone = true;
            yield break;
        }

    }

}