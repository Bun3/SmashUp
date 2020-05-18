using UnityEngine;
using System;

public enum Scene
{
    Main, CharShop, SkinShop, LandShop, InGame
}

namespace Zero
{
    public class SceneManager : MonoBehaviour
    {
        private void Start()
        {
            ChangeScene(Scene.Main);
        }

        public void ChangeScene(Scene scene, Action func = null)
        {
            GameObject zone = Singleton.game.sceneUIZone;

            Transform shouldChangeScene = zone.transform.Find(("[" + scene.ToString() + "](Clone)"));
            if (shouldChangeScene == null) 
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/Scene/" + "[" + scene.ToString() + "]"), zone.transform);
            }

            for (int i = 0; i < zone.transform.childCount; i++)
            {
                string currentScene = "[" + scene.ToString() + "](Clone)";
                zone.transform.GetChild(i).gameObject.SetActive(currentScene.Equals(zone.transform.GetChild(i).name));
            }

            func?.Invoke();
        }
    }
}