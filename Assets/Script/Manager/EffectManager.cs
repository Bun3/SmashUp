using System.Collections;
using UnityEngine;

namespace Zero
{

    public class EffectManager : MonoBehaviour
    {

        public void Play(string effectName)
        {
            StartCoroutine(IPlay(effectName));
        }
        public void Play(string effectName, Transform parent)
        {
            StartCoroutine(IPlay(effectName, parent));
        }
        public void Play(string effectName, Vector3 position, Quaternion rotation)
        {
            StartCoroutine(IPlay(effectName, position, rotation));
        }
        public void Play(string effectName, Vector3 position, Quaternion rotation, Transform parent)
        {
            StartCoroutine(IPlay(effectName, position, rotation, parent));
        }

        private IEnumerator IPlay(string name)
        {
            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/[" + name + "]"));
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            yield return new WaitWhile(() => { return ps.isPlaying; });

            Destroy(effect);
        }
        private IEnumerator IPlay(string name, Transform p)
        {
            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/[" + name + "]"), p);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            yield return new WaitWhile(() => { return ps.isPlaying; });

            Destroy(effect);
        }
        private IEnumerator IPlay(string name, Vector3 position, Quaternion rotation)
        {
            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/[" + name + "]"), position, rotation);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            yield return new WaitWhile(() => { return ps.isPlaying; });

            Destroy(effect);
        }
        private IEnumerator IPlay(string name, Vector3 position, Quaternion rotation, Transform p)
        {
            GameObject effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/[" + name + "]"), position, rotation, p);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            yield return new WaitWhile(() => { return ps.isPlaying; });

            Destroy(effect);
        }
    }

}