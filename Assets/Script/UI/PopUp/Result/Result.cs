using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Zero
{

    public class Result : MonoBehaviour
    {
        private int score;
        private int gold;

        [SerializeField]
        private GameObject newBest = null;
        [SerializeField]
        private GameObject oldBest = null;
        [SerializeField]
        private Text scoreText = null;
        [SerializeField]
        private Text goldText = null;
        [SerializeField]
        private RectTransform adsRt = null;

        private Text newBestText;
        private Text oldBestText;

        private void Awake()
        {
            InGame ingame = GameObject.Find("[InGame](Clone)").GetComponent<InGame>();
            score = ingame.Score;
            gold = ingame.Gold;

            newBestText = newBest.GetComponent<Text>();
            oldBestText = oldBest.GetComponent<Text>();

            Singleton.game.character.IsLookLeft = false;

            scoreText.text = score.ToString();
            goldText.text = gold.ToString();

            DataManager.GameData.TotalScore += score;

            if (DataManager.GameData.CharacterBestScore[DataManager.GameData.CharIndex - 1] < score)
            {
                DataManager.GameData.CharacterBestScore[DataManager.GameData.CharIndex - 1] = score;
            }
            SortBestScore();
        }

        private void Start()
        {
            if (score > DataManager.GameData.HighScore)
            {
                DataManager.GameData.HighScore = score;
                oldBest.transform.parent.gameObject.SetActive(false);
                newBest.SetActive(true);
                StartCoroutine(INewBest());
            }
            else
            {
                newBest.SetActive(false);
                oldBestText.text = DataManager.GameData.HighScore.ToString();
                oldBest.transform.parent.gameObject.SetActive(true);
            }
            StartCoroutine(IRewardButtonEffect());
        }

        private IEnumerator INewBest()
        {
            newBestText.color = new Color(1, 0.84f, 0.5f, 1);

            yield return new WaitForSeconds(0.8f);

            while (this.gameObject.activeSelf)
            {
                newBestText.color = new Color(1, 0.84f, 0.5f, Mathf.PingPong(Time.time / 2, 1));
                yield return null;
            }
        }

        private IEnumerator IRewardButtonEffect()
        {
            yield return new WaitForSeconds(0.5f);

            adsRt.localScale = Vector3.one * 1.1f;

            yield return new WaitForSeconds(0.5f);

            adsRt.localScale = Vector3.one * 1.2f;

            float timer = 0f;
            while (adsRt.localScale != Vector3.one)
            {
                adsRt.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one, timer / 1f);

                timer += Time.smoothDeltaTime;

                yield return null;
            }

            yield return StartCoroutine(IRewardButtonEffect());
        }

        private void SortBestScore()
        {
            int tmp = 0;

            //HighScoreFind
            for (int i = 0; i < DataManager.define_AllCharCnt; i++)
                if (tmp < DataManager.GameData.CharacterBestScore[i])
                    tmp = DataManager.GameData.CharacterBestScore[i];

            //IndexFind
            for (int i = 0; i < DataManager.define_AllCharCnt; i++)
                if(tmp.Equals(DataManager.GameData.CharacterBestScore[i]))
                    DataManager.GameData.BestScoreCharIndex = i + 1;
        }

        public void Get()
        {
            DataManager.GameData.Gold += gold;
            CloseResultPopUp();
            GetCoinPopUpInstance().CoinCountUp(0, gold);
        }

        public void GetDouble()
        {
            Singleton.ad.ShowRewardAd();
        }

        public void CloseResultPopUp()
        {
            Singleton.game.character.Animator.SetBool("bDeath", true);
            Destroy(this.gameObject);

            Singleton.sound.BgmSource.mute = false;
            Singleton.sound.BgmSource.Play();
            Singleton.scene.ChangeScene(Scene.Main);
        }

        public void GetDoubleReward()
        {
            DataManager.GameData.Gold += gold * 2;
            CloseResultPopUp();
            GetCoinPopUpInstance().CoinCountUp(gold, gold * 2);
        }

        private DoubleCoin GetCoinPopUpInstance() { return Instantiate(Resources.Load<GameObject>("Prefabs/PopUp/[DoubleCoin]").GetComponent<DoubleCoin>(),GameObject.Find("[PopUpUIZone]").transform); }
    }

}