using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject quitPopUp = null;
        [SerializeField] private GameObject networkPopUp = null;

        private GameObject popUpUiZone = null;

        public GameObject PopUpUiZone { get => popUpUiZone; set => popUpUiZone = value; }

        private void Awake()
        {
            quitPopUp.SetActive(false);
            networkPopUp.SetActive(false);

            popUpUiZone = GameObject.Find("[PopUpUIZone]");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                if (Time.timeScale.Equals(0) && quitPopUp.activeSelf)
                    CloseQuitPopUp();
                else
                    MakeQuitPopUp();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                networkPopUp.SetActive(!networkPopUp.activeSelf);
                Time.timeScale = System.Convert.ToInt32(!(Singleton.sound.BgmSource.mute = networkPopUp.activeSelf));
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                DataManager.GameData.Gold += 99999;
            }

            if (Application.internetReachability.Equals(NetworkReachability.NotReachable) && !networkPopUp.activeSelf)
            {
                print("허걱!");
                networkPopUp.SetActive(true);
                Time.timeScale = System.Convert.ToInt32(!(Singleton.sound.BgmSource.mute = networkPopUp.activeSelf));
            }
            else if(!Application.internetReachability.Equals(NetworkReachability.NotReachable) && networkPopUp.activeSelf)
            {
                print("다행이다");
                networkPopUp.SetActive(false);
                Time.timeScale = System.Convert.ToInt32(!(Singleton.sound.BgmSource.mute = networkPopUp.activeSelf));
            }

        }

        private void MakeQuitPopUp()
        {
            quitPopUp.SetActive(true);
            Time.timeScale = 0;
        }
        public void CloseQuitPopUp()
        {
            quitPopUp.SetActive(false);
            Time.timeScale = 1;
        }
    }

}