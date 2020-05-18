using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Zero
{

    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public GameObject backGround = null;
        [HideInInspector]
        public GameObject sceneUIZone = null;
        [HideInInspector]
        public Character character = null;
        [HideInInspector]
        public SpriteAtlas spriteAtlas = null;
        [HideInInspector]
        public Dictionary<string, GameObject> fallObjects = new Dictionary<string, GameObject>();

        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;

            character = GameObject.Find("[Character]").GetComponent<Character>();
            backGround = GameObject.FindGameObjectWithTag("BackGround");
            sceneUIZone = GameObject.Find("[SceneUIZone]");

            spriteAtlas = Resources.Load<SpriteAtlas>("Sprite/Atlas/UIAtlas");

            #region EveryFallObjectLoading

            //by LandCount + 1 
            for (int landIndex = 0; landIndex < DataManager.define_AllLandCnt + 1; landIndex++)
            {
                //by ObjectMaxCount
                for (int objectIndex = 0; objectIndex < 3; objectIndex++)
                {
                    fallObjects.Add(
                        "[" + landIndex.ToString() + "][E][" + (objectIndex + 1).ToString() + "]",
                        Resources.Load<GameObject>("Prefabs/Building/[" + landIndex.ToString() + "][E][" + (objectIndex + 1).ToString() + "]"));

                    if (objectIndex < 2)
                        fallObjects.Add(
                            "[" + landIndex.ToString() + "][N][" + (objectIndex + 1).ToString() + "]",
                            Resources.Load<GameObject>("Prefabs/Building/[" + landIndex.ToString() + "][N][" + (objectIndex + 1).ToString() + "]"));
                }

                //is not base
                if (landIndex > 0)
                    fallObjects.Add("[" + landIndex.ToString() + "][H][1]",
                        Resources.Load<GameObject>("Prefabs/Building/[" + landIndex.ToString() + "][H][1]"));
            }

            #endregion
        }

        #region 앱 처음 실행 시 터치미스 방지

        private void Start()
        {
            backGround.GetComponent<SpriteRenderer>().sprite =
                    Resources.Load<Sprite>("Sprite/GameObject/BackGround/[" + (DataManager.GameData.LandIndex + 1).ToString() + "]");
            StartCoroutine(BlockMistake());
        }
        IEnumerator BlockMistake()
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(GameObject.Find("[BlockMistake]"));
        }

        #endregion

        #region Data Save

        private void OnApplicationQuit()
        {
            DataManager.Inst.SaveData();
        }
        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
                DataManager.Inst.SaveData();
        }
        private void OnApplicationPause(bool pause)
        {
            if (pause)
                DataManager.Inst.SaveData();
        }

        #endregion
    }

}