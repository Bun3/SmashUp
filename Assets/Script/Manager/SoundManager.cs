using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{

    public class SoundManager : MonoBehaviour
    {
        private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
        private AudioSource bgmSource;

        [SerializeField]
        private GameObject soundZone = null;

        public AudioSource BgmSource { get => bgmSource; set => bgmSource = value; }

        private void Awake()
        {
            bgmSource = Camera.main.GetComponent<AudioSource>();

            clips.Add("BGM", Load("MainBGM"));
            clips.Add("Click", Load("ButtonClick"));
            clips.Add("Attack", Load("Attack"));
            clips.Add("Coin", Load("Coin"));
            clips.Add("Die", Load("Death"));
            clips.Add("De", Load("D"));
            clips.Add("Explosion", Load("Explosion"));
            clips.Add("Fall", Load("Fall"));

        }

        private void Start()
        {
            Play("BGM", true);
        }

        public void Play(string name, bool bgm = false, float vol = 1f)
        {
            StartCoroutine(ICreateAndPlay(name, bgm, vol));
        }

        private IEnumerator ICreateAndPlay(string name, bool bgm, float vol)
        {
            if (!bgm)
            {
                AudioSource source = Instantiate(Resources.Load<GameObject>("Prefabs/Sound/[SoundEffect]"), soundZone.transform).GetComponent<AudioSource>();

                source.mute = !DataManager.GameData.SoundOn;
                source.clip = clips[name];
                source.loop = false;
                source.volume = vol;

                source.Play();

                yield return new WaitWhile(() => { return source.isPlaying; });

                Destroy(source);
            }
            else
            {
                bgmSource.mute = !DataManager.GameData.MusicOn;
                bgmSource.clip = clips[name];
                bgmSource.volume = vol;
                bgmSource.loop = true;

                bgmSource.Play();

                yield return null;
            }
        }

        private AudioClip Load(string key)
        {
            return Resources.Load<AudioClip>("Sound/[" + key + "]");
        }

    }

}
