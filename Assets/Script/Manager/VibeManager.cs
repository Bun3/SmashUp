using System.Collections;
using UnityEngine;

namespace Zero
{

    public class VibeManager : MonoBehaviour
    {

        public void Viberation(long ms = 1000)
        {
            if (DataManager.GameData.VibeOn && !Time.timeScale.Equals(0))
            {
                //if (IsAndroid())
                //    vibrator.Call("vibrate", ms);
                //else
                    StartCoroutine(IViberation());
            }
        }

        private IEnumerator IViberation()
        {
            Handheld.Vibrate();
            yield return null;
        }

    }

}