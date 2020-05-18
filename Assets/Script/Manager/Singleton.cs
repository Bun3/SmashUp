using UnityEngine;

namespace Zero
{

    [RequireComponent(typeof(CommandManager))]
    [RequireComponent(typeof(EffectManager))]
    [RequireComponent(typeof(SoundManager))]
    [RequireComponent(typeof(SceneManager))]
    [RequireComponent(typeof(GameManager))]
    [RequireComponent(typeof(VibeManager))]
    [RequireComponent(typeof(AdManager))]
    [RequireComponent(typeof(UIManager))]
    public class Singleton : MonoBehaviour
    {
        public static Singleton Instance = null;

        public static CommandManager command;
        public static EffectManager effect;
        public static SoundManager sound;
        public static SceneManager scene;
        public static GameManager game;
        public static VibeManager vibe;
        public static AdManager ad;
        public static UIManager ui;

        private void Awake()
        {
            #region GetInstance

            if (Instance != null)
                Destroy(Instance);
            Instance = this;

            #endregion

            #region ManagerParsing

            command = GetComponent<CommandManager>();
            effect = GetComponent<EffectManager>();
            sound = GetComponent<SoundManager>();
            scene = GetComponent<SceneManager>();
            game = GetComponent<GameManager>();
            vibe = GetComponent<VibeManager>();
            ad = GetComponent<AdManager>();
            ui = GetComponent<UIManager>();

            #endregion;
        }
    }

}