using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Zero
{

    public class Commands
    {
        private Image image;
        private RectTransform rect;

        public Commands(GameObject obj)
        {
            image = obj.GetComponent<Image>();
            rect = obj.GetComponent<RectTransform>();
        }

        public Image Image { get => image; set => image = value; }
        public RectTransform Rect { get => rect; set => rect = value; }
    }

    public class InGame : MonoBehaviour
    {
        private DIFFICULTY nowDifficulty;

        private Queue<FallObject> fallObjects = new Queue<FallObject>();

        private GameObject[] cmdPrefabs = new GameObject[2];

        private Queue<Commands> cmds = new Queue<Commands>();
        private Image[] buttons = new Image[2];

        private Slider timeGauge;
        private Image timefillGauge;
        float timeGaugeTimer;

        [SerializeField]
        private RectTransform feverMode = null;
        private Image feverGauge;

        private RectTransform scoreRt;
        private Text scoreText;

        [SerializeField]
        private Text goldText = null;

        private Coroutine c_FeverGaugeUpdate = null;

        [SerializeField]
        private InputButton[] inputButtons = null;

        private const float m_feverCalculationVal = 0.01f;

        private const int m_dis = 200;
        private const float m_feverUpValue = 10f * m_feverCalculationVal;
        private const float m_inputTime = 4f;
        private const float m_feverDownFlow = 0.5f * m_feverCalculationVal;
        private const float m_maxFeverValue = 100f * m_feverCalculationVal;

        private bool isFeverMode = false;

        private static int gold = 0;
        private static int score = 0;
        private static int playCnt = 0;
        private static int m_OldScore = 0;
        private static int m_goldUpUnit = 0;
        private static int m_ScoreUpUnit = 5;
        private static int m_ScoreUnit = 500;
        private static int m_AllClearScore = 0;
        private const float m_inputTimeCharge = 0.1f;
        private const float m_resultPopUpCreateTime = 2f;

        [SerializeField] private Color32 fillColor = Color.white;
        [SerializeField] private Color32 normalTextColor = Color.white;
        [SerializeField] private Color32 effectTextColor = Color.white;
        //private static Color32[] textColors = new Color32[] { new Color32(0, 207, 255, 255), new Color32(255, 108, 109, 255) };

        public int Score { get => score; set => score = value; }
        public int Gold { get => gold; set => gold = value; }


        private void Awake()
        {
            buttons[0] = transform.Find("[Left]").GetComponent<Image>();
            buttons[1] = transform.Find("[Right]").GetComponent<Image>();

            for (int i = 0; i < cmdPrefabs.Length; i++)
                cmdPrefabs[i] = Resources.Load<GameObject>("Prefabs/Command/[" + (i + 1) + "]");

            timeGauge = transform.Find("[TimeGauge]").GetComponent<Slider>();
            timefillGauge = timeGauge.gameObject.transform.GetChild(0).GetComponent<Image>();

            feverGauge = transform.Find("[FeverGauge]").GetChild(0).GetComponent<Image>();

            scoreRt = transform.Find("[Score]").GetComponent<RectTransform>();
            scoreText = scoreRt.gameObject.GetComponent<Text>();
        }

        private void OnEnable()
        {
            m_AllClearScore = 0;
            m_ScoreUnit = 500;
            m_ScoreUpUnit = 5;
            m_goldUpUnit = 0;
            m_OldScore = 0;

            gold = 0;
            goldText.text = gold.ToString();
            score = 0;
            scoreText.text = score.ToString();

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].sprite = Singleton.game.spriteAtlas.GetSprite("[Button][" + DataManager.GameData.SkinIndex + "][" + (i + 1) + "]");

            cmds.Clear();
            fallObjects.Clear();
            Singleton.command.InputQ.Clear();
            Singleton.command.PatternQ.Clear();

            timeGauge.value = 1f;
            timefillGauge.color = fillColor;

            feverGauge.fillAmount = 0f;
            feverMode.gameObject.SetActive(false);

            StartCoroutine(ISpawnObject());
            if (Application.platform.Equals(RuntimePlatform.WindowsEditor)) StartCoroutine(IInputCommandToKey());
            c_FeverGaugeUpdate = StartCoroutine(IFeverGaugeUpdate());
            Singleton.game.character.Animator.SetBool("bDeath", false);
        }

        #region Dont use other CS, Only use here

        private IEnumerator ISpawnObject(float waitTime = 0.0f)
        {
            yield return new WaitForSeconds(waitTime);

            nowDifficulty = Singleton.command.GetRandomDifficulty();
            Singleton.command.GetRandomPattern(nowDifficulty);

            StartCoroutine(ICreateCommand());

            int first = GetBaseOrLand(System.Convert.ToBoolean(Random.Range(0, 2)));
            char second = nowDifficulty.ToString()[11];
            int third = GetRandomObjectIndex(nowDifficulty);

            GameObject dropObject = Singleton.game.fallObjects["[" +
                first + "][" +
                second + "][" +
                third + "]"];

            fallObjects.Enqueue(Instantiate(dropObject).GetComponent<FallObject>());
        }

        private IEnumerator IInputCommandToKey()
        {
            while (gameObject.activeSelf)
            {
                if (Singleton.command.IsInputTime)
                {

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Singleton.command.InputQ.Enqueue((int)DIRECTION.DIRECTION_LEFT);

                        if (Singleton.command.CompareNowInput())
                            StartCoroutine(IInputSuccess());
                        else
                            StartCoroutine(IGameOver());

                        if (Singleton.command.CompareNowPattern())
                            StartCoroutine(IInputAllSuccess());
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Singleton.command.InputQ.Enqueue((int)DIRECTION.DIRECTION_RIGHT);

                        if (Singleton.command.CompareNowInput())
                            StartCoroutine(IInputSuccess());
                        else
                            StartCoroutine(IGameOver());

                        if (Singleton.command.CompareNowPattern())
                            StartCoroutine(IInputAllSuccess());
                    }

                    if (Input.GetKey(KeyCode.Space))
                    {
                        Singleton.command.InputQ.Enqueue(Singleton.command.PatternQ.Peek());

                        if (Singleton.command.CompareNowInput())
                            StartCoroutine(IInputSuccess());

                        if (Singleton.command.CompareNowPattern())
                            StartCoroutine(IInputAllSuccess());
                    }
                }
                yield return null;
            }
            yield break;
        }

        private bool GetPercentage(int p)
        {
            return Random.Range(0, 100) < p;
        }

        private IEnumerator IWaitFrame(int cnt)
        {
            for (int i = 0; i < cnt; i++)
                yield return null;
        }

        private IEnumerator IFeverGaugeUpdate()
        {
            yield return new WaitForSeconds(1f);

            print("FeverUpdate");

            if (feverGauge.fillAmount > 0f && !isFeverMode)
                feverGauge.fillAmount -= m_feverDownFlow;

            if (feverGauge.fillAmount <= 0f)
                feverGauge.fillAmount = 0f;

            c_FeverGaugeUpdate = StartCoroutine(IFeverGaugeUpdate());
            yield break;
        }

        private IEnumerator IUpdateScore(int addScore = 0)
        {
            score += addScore;
            scoreText.text = score.ToString();

            scoreRt.localScale = Vector3.one * 1.5f;
            scoreText.color = effectTextColor;

            yield return StartCoroutine(IWaitFrame(5));

            scoreRt.localScale = Vector3.one;
            scoreText.color = normalTextColor;
        }

        private IEnumerator IFillGaugeEffect()
        {
            timefillGauge.color = Color.white;

            yield return StartCoroutine(IWaitFrame(2));

            timefillGauge.color = fillColor;
        }

        private IEnumerator ICreateCommand()
        {
            for (int i = 0; i < Singleton.command.PatternQ.Count; i++)
                cmds.Enqueue(new Commands(Instantiate(cmdPrefabs[Singleton.command.PatternArr[i] - 1], this.transform)));

            StartCoroutine(ISetCommandPos());

            yield break;
        }

        private IEnumerator ISetCommandPos()
        {
            IEnumerator cmdEtor = cmds.GetEnumerator();
            for (int i = 0; i < cmds.Count; i++)
            {
                Vector2 newPos = new Vector2(i * m_dis, 400);
                cmdEtor.MoveNext();
                ((Commands)cmdEtor.Current).Rect.anchoredPosition = newPos;
            }
            yield break;
        }

        private IEnumerator IPopCommand()
        {
            float setTime = 0.3f;
            float timer = 0f;

            Commands popCmd = cmds.Dequeue();
            popCmd.Image.transform.SetSiblingIndex(2);

            StartCoroutine(ISetCommandPos());

            byte col = 150;
            while (timer <= setTime)
            {
                popCmd.Image.color = new Color32(col, col, col, (byte)Mathf.Lerp(255, 0, timer / setTime));
                popCmd.Rect.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, timer / setTime);

                timer += Time.smoothDeltaTime;

                yield return null;
            }

            Destroy(popCmd.Image.gameObject);
        }

        public IEnumerator ITimeCheck()
        {
            timeGaugeTimer = 0f;
            while (Singleton.command.IsInputTime && timeGaugeTimer < m_inputTime)
            {
                timeGauge.value = Mathf.Lerp(1, 0, timeGaugeTimer / m_inputTime);
                timeGaugeTimer += Time.deltaTime;
                yield return null;
            }

            if (timeGaugeTimer >= m_inputTime)
                StartCoroutine(IGameOver());

            timeGauge.value = 1f;
        }

        private int GetBaseOrLand(bool isBase)
        {
            return isBase && nowDifficulty != DIFFICULTY.DIFFICULTY_HARD ? 0 : DataManager.GameData.LandIndex + 1;
        }

        private int GetRandomObjectIndex(DIFFICULTY d)
        {
            if (d == DIFFICULTY.DIFFICULTY_EASY)
                return Random.Range(1, 4);
            else if (d == DIFFICULTY.DIFFICULTY_NORMAL)
                return Random.Range(1, 3);
            else
                return 1;
        }

        private IEnumerator IFeverMode()
        {
            feverGauge.fillAmount = m_maxFeverValue;
            feverMode.gameObject.SetActive(isFeverMode = true);

            float feverModeOffset = 150f;

            float initTime = .2f;
            float initTimer = 0f;
            while (initTimer <= initTime)
            {
                feverMode.offsetMin = Vector2.Lerp(Vector2.one * -feverModeOffset, Vector2.zero, initTimer / initTime);
                feverMode.offsetMax = Vector2.Lerp(Vector2.one *  feverModeOffset, Vector2.zero, initTimer / initTime);
                initTimer += Time.smoothDeltaTime;
                yield return null;
            }
            feverMode.offsetMin = feverMode.offsetMax = Vector2.zero;

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].sprite = Singleton.game.spriteAtlas.GetSprite("[Button][Fever]");

            float totalfeverTime = 10f;
            float feverOutroTime = 3f;

            float feverModeTimer = totalfeverTime;
            while (isFeverMode)
            {
                feverGauge.fillAmount = Mathf.Lerp(0, m_maxFeverValue, feverModeTimer / totalfeverTime);

                if (feverModeTimer <= feverOutroTime)
                {
                    feverMode.offsetMin = Vector2.Lerp(Vector2.one * -feverModeOffset, Vector2.zero, feverModeTimer / feverOutroTime);
                    feverMode.offsetMax = Vector2.Lerp(Vector2.one *  feverModeOffset, Vector2.zero, feverModeTimer / feverOutroTime);
                }
                if (feverModeTimer <= 0f) feverMode.gameObject.SetActive(isFeverMode = false);

                feverModeTimer -= Time.smoothDeltaTime;
                yield return null;
            }
            feverMode.offsetMin = Vector2.one * -feverModeOffset;
            feverMode.offsetMax = Vector2.one *  feverModeOffset;

            for (int i = 0; i < buttons.Length; i++)
                buttons[i].sprite = Singleton.game.spriteAtlas.GetSprite("[Button][" + DataManager.GameData.SkinIndex + "][" + (i + 1) + "]");

            yield break;
        }

        #endregion

        public void DownOnFeverMode() => inputButtons[Singleton.command.PatternQ.Peek() - 1].Down();
        public void UpOnFeverMode() => inputButtons[Singleton.command.PatternQ.Peek() - 1].Up();

        public void ClickOnFeverMode()
        {
            Singleton.command.InputQ.Enqueue(Singleton.command.PatternQ.Peek());

            if (Singleton.command.CompareNowInput())
                StartCoroutine(IInputSuccess());

            if (Singleton.command.CompareNowPattern())
                StartCoroutine(IInputAllSuccess());
        }

        public IEnumerator IInputSuccess()
        {
            Singleton.effect.Play("HitEffect", fallObjects.Peek().transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);
            Singleton.sound.Play("Attack");

            StartCoroutine(CameraAction.Instance.Shake(0.1f, 0.8f));
            StartCoroutine(fallObjects.Peek().ICracking());
            StartCoroutine(IFillGaugeEffect());

            timeGaugeTimer -= m_inputTimeCharge;

            if (!isFeverMode)
            {
                feverGauge.fillAmount += m_feverUpValue;

                if (feverGauge.fillAmount >= m_maxFeverValue)
                    StartCoroutine(IFeverMode());
            }

            StartCoroutine(IUpdateScore(m_ScoreUpUnit));
            StartCoroutine(IPopCommand());

            Singleton.game.character.IsLookLeft = !Singleton.game.character.IsLookLeft;
            Singleton.game.character.Animator.SetTrigger("tAttack");

            yield break;
        }

        private void UpdateGold(int percent, int minGold, int maxGold)
        {
            if (GetPercentage(percent))
            {
                gold += Random.Range(minGold, maxGold + 1) + m_goldUpUnit;
                goldText.text = gold.ToString();
                Singleton.sound.Play("Coin");
                Singleton.effect.Play("Coin", fallObjects.Peek().transform.position, Quaternion.identity);
            }
        }

        public IEnumerator IInputAllSuccess()
        {
            Singleton.command.EndInputTime();
            if (score >= m_ScoreUnit && m_OldScore < m_ScoreUnit)
            {
                m_goldUpUnit++;
                m_ScoreUnit *= 2;
                m_ScoreUpUnit += 5;
                m_AllClearScore += 5;
            }
            m_OldScore = score;

            switch (nowDifficulty)
            {
                case DIFFICULTY.DIFFICULTY_EASY:
                    score += 10 + m_AllClearScore;
                    UpdateGold(50, 1, 2);
                    break;
                case DIFFICULTY.DIFFICULTY_NORMAL:
                    score += 15 + m_AllClearScore;
                    UpdateGold(70, 2, 3);
                    break;
                case DIFFICULTY.DIFFICULTY_HARD:
                    score += 30 + m_AllClearScore;
                    UpdateGold(100, 4, 6);
                    break;
                default:
                    break;
            }
            scoreText.text = score.ToString();

            Singleton.sound.Play("Explosion");
            StartCoroutine(fallObjects.Dequeue().IExplosion());

            StartCoroutine(ISpawnObject());

            yield break;
        }

        public IEnumerator IGameOver()
        {
            Singleton.command.EndInputTime();

            Singleton.sound.Play("Fall");

            Singleton.game.character.Animator.SetTrigger("tDeath");

            StartCoroutine(CameraAction.Instance.Shake(0.5f, 3f));
            StartCoroutine(fallObjects.Dequeue().IFail());
            StopCoroutine(c_FeverGaugeUpdate);

            Singleton.effect.Play("Smoke");
            Singleton.vibe.Viberation();

            playCnt++;

            yield return new WaitForSeconds(m_resultPopUpCreateTime);
            Singleton.sound.BgmSource.mute = true;
            yield return new WaitForSeconds(0.2f);
            Singleton.sound.Play("De");

            if (playCnt.Equals(3))
            {
                Singleton.ad.ShowFrontAd();
                playCnt = 0;
            }

            Instantiate(Resources.Load<GameObject>("Prefabs/PopUp/[Result]"), GameObject.Find("[PopUpUIZone]").transform);

            int leftCmdCnt = cmds.Count;
            for (int i = 0; i < leftCmdCnt; i++)
                Destroy(cmds.Dequeue().Image.gameObject);
        }

    }

}