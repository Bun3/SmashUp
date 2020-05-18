using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Zero
{

    public class RankingButton : MonoBehaviour, IPointerClickHandler
    {
        private GameObject rankingPopUp = null;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (rankingPopUp == null)
            {
                GameObject rank = Resources.Load<GameObject>("Prefabs/PopUp/[Ranking]");
                rankingPopUp = Instantiate(rank, GameObject.Find("[PopUpUIZone]").transform);
            }
            else
                rankingPopUp.SetActive(true);
        }
    }

}