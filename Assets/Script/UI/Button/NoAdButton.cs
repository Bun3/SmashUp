using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Zero
{

    public class NoAdButton : CustomButton
    {
        private IAPButton iap = null;

        private void Awake()
        {
            iap = GetComponent<IAPButton>();
        }

        private void OnEnable() => iap.gameObject.SetActive(!DataManager.GameData.HasNoAdItem);

        public void OnCompleted(Product product)
        {
            DataManager.GameData.HasNoAdItem = true;
            iap.gameObject.SetActive(!DataManager.GameData.HasNoAdItem);
        }

        public void OnFailed(Product product, PurchaseFailureReason reason)
        {
            print(reason);
        }

    }

}