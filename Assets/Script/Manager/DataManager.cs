using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Zero
{
    [Serializable]
    public class PurChaseInfo
    {
        public int Need;
        public bool IsCollected;

        public PurChaseInfo(int n = 0, bool isCollect = false)
        {
            Need = n;
            IsCollected = n.Equals(0) ? true : isCollect;
        }
    }

    [Serializable]
    public class GameData
    {
        public bool HasNoAdItem;
        public bool MusicOn;
        public bool SoundOn;
        public bool VibeOn;

        public int TotalScore;
        public int HighScore;
        public int Gold;

        public int SkinIndex;
        public int CharIndex;
        public int LandIndex;

        public int[] CharacterBestScore;
        public int BestScoreCharIndex;

        public List<PurChaseInfo>[] PurChases = new List<PurChaseInfo>[3];

        public GameData(int charCnt)
        {
            HasNoAdItem = false;
            MusicOn = true;
            SoundOn = true;
            VibeOn = true;

            TotalScore = 0;
            HighScore = 0;
            Gold = 500;

            SkinIndex = 1;
            CharIndex = 1;
            LandIndex = 0;

            CharacterBestScore = new int[charCnt];
            CharacterBestScore.Initialize();

            for (int i = 0; i < PurChases.Length; i++)
                PurChases[i] = new List<PurChaseInfo>();

            #region Character

            PurChases[0].Add(new PurChaseInfo());
            PurChases[0].Add(new PurChaseInfo(250));
            PurChases[0].Add(new PurChaseInfo(400));
            PurChases[0].Add(new PurChaseInfo(600));
            PurChases[0].Add(new PurChaseInfo(1000));

            #endregion

            #region ButtonSkin

            PurChases[1].Add(new PurChaseInfo());
            PurChases[1].Add(new PurChaseInfo(150));
            PurChases[1].Add(new PurChaseInfo(300));
            PurChases[1].Add(new PurChaseInfo(450));
            PurChases[1].Add(new PurChaseInfo(600));

            #endregion

            #region Stage

            PurChases[2].Add(new PurChaseInfo());
            PurChases[2].Add(new PurChaseInfo(500));
            PurChases[2].Add(new PurChaseInfo(1000));

            #endregion
        }
    }

    public class DataManager : MonoBehaviour
    {
        public static int define_AllCharCnt = 5;
        public static int define_AllLandCnt = 3;
        public static int define_AllSkinCnt = 5;

        private string path = string.Empty;

        private static GameData gameData = null;

        public static GameData GameData { get => gameData; set => gameData = value; }
        public static DataManager Inst = null;


        private void Awake()
        {
            path = Application.persistentDataPath + "/SmashUpGameData.txt";
            print(path);
            if (Inst != null) Destroy(Inst);
            Inst = this;

            if(File.Exists(path))
            {
                gameData = LoadData();
            }
            else
            {
                gameData = new GameData(define_AllCharCnt);
            }

        }

        public void SaveData()
        {
            File.WriteAllText(path, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(gameData))));
        }
        public GameData LoadData()
        {
            return JsonConvert.DeserializeObject<GameData>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(path))));
        }

        [ContextMenu("ClearData")]
        public void ClearData()
        {
            print("!!!");
            //gameData = new GameData(define_AllCharCnt);
            //SaveData();
        }
    }
}