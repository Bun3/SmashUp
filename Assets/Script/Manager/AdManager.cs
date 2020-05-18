using GoogleMobileAds.Api;
using System;
using UnityEngine;


namespace Zero
{

    public class AdManager : MonoBehaviour
    {
        private InterstitialAd frontAd;
        private RewardedAd rewardAd;

        #region AdMobIds

        private string appId = "ca-app-pub-5708876822263347~1820588561";
        private string frontAdUnitId;
        private string rewardAdUnitId;

        #endregion;

        [SerializeField]
        private bool isTest = true;

        private void Start()
        {

            if (isTest)
            {
                frontAdUnitId = "ca-app-pub-3940256099942544/1033173712";
                rewardAdUnitId = "ca-app-pub-3940256099942544/5224354917";
            }
            else
            {
                frontAdUnitId = "ca-app-pub-5708876822263347/6306628488";
                rewardAdUnitId = "ca-app-pub-5708876822263347/8108597068";
            }

            MobileAds.Initialize(appId);

            RequestFrontAd();
            RequestRewardAd();
        }

        public void RequestFrontAd()
        {
            frontAd = new InterstitialAd(frontAdUnitId);
            print("FrontAd: " + frontAd);

            // 광고가 끝날 때
            frontAd.OnAdClosed += FrontOnAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            frontAd.LoadAd(request);
        }

        public void RequestRewardAd()
        {
            rewardAd = new RewardedAd(rewardAdUnitId);

            // Called when an ad is shown.
            rewardAd.OnAdOpening += OnRewardAdOpening;
            // Called when the user should be rewarded for interacting with the ad.
            rewardAd.OnUserEarnedReward += OnRewardUserEarnedReward;
            // Called when the ad is closed.
            rewardAd.OnAdClosed += OnRewardAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            rewardAd.LoadAd(request);
        }

        public void ShowFrontAd()
        {
            if (frontAd.IsLoaded() && !DataManager.GameData.HasNoAdItem)
                frontAd.Show();
            else
                print("Not Loaded Yet");
        }
        public void ShowRewardAd()
        {
            if (rewardAd.IsLoaded())
                rewardAd.Show();
            else
                print("Not Loaded Yet");
        }

        #region FrontEventers

        public void FrontOnAdClosed(object sender, EventArgs args)
        {
            print("HandleAdClosed event received");

            frontAd.Destroy();

            RequestFrontAd();
        }

        #endregion

        #region RewardEventers

        public void OnRewardAdOpening(object sender, EventArgs args)
        {
            print("HandleRewardedAdOpening event received");
        }

        public void OnRewardAdClosed(object sender, EventArgs args)
        {
            rewardAd = null;
            RequestRewardAd();
            GameObject.Find("[Result](Clone)").GetComponent<Result>().CloseResultPopUp();
            print("HandleRewardedAdClosed event received");
        }

        public void OnRewardUserEarnedReward(object sender, Reward args)
        {
            string type = args.Type;
            GameObject.Find("[Result](Clone)").GetComponent<Result>().GetDoubleReward();
            print("HandleRewardedAdRewarded event received");
        }

        #endregion
    }

}