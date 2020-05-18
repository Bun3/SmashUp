using System.Collections;
using UnityEngine;
using System;

namespace Zero
{
    public class CameraAction : MonoBehaviour
    {
        public static CameraAction Instance = null;

        float m_beginCamSize = 5f;
        float m_nowCamSize = 5f;

        Vector3 m_beginCamPos = new Vector3(0, 0, -10);
        Vector3 m_nowCamPos = new Vector3(0, 0, -10);

        Camera m_cam = null;

        public float BeginCamSize { get => m_beginCamSize; set => m_beginCamSize = value; }
        public float NowCamSize { get => m_nowCamSize; set => m_nowCamSize = value; }
        public Vector3 BeginCamPos { get => m_beginCamPos; set => m_beginCamPos = value; }
        public Vector3 NowCamPos { get => m_nowCamPos; set => m_nowCamPos = value; }

        private void Awake()
        {
            #region MakeSingleton

            if (Instance != null)
                Destroy(Instance);

            Instance = this;

            #endregion

            m_cam = GetComponent<Camera>();

        }

        public IEnumerator IZoomIn(float totalTime, float targetSize, Vector3 targetPos, Action func = null)
        {
            float time = 0f;
            while (time <= totalTime)
            {
                print("!");
                m_cam.transform.position = Vector3.Lerp(m_nowCamPos, targetPos, (time / totalTime));
                m_cam.orthographicSize = Mathf.Lerp(m_nowCamSize, targetSize, (time / totalTime));

                time += Time.smoothDeltaTime;

                yield return null;
            }

            m_cam.orthographicSize = m_nowCamSize = targetSize;
            m_cam.transform.position = m_nowCamPos = targetPos;

            func?.Invoke();
        }

        public IEnumerator IZoomIn(float targetSize, Vector3 targetPos, Action func = null)
        {
            m_cam.orthographicSize = m_nowCamSize = targetSize;
            m_cam.transform.position = m_nowCamPos = targetPos;

            yield return null;

            func?.Invoke();
        }

        public IEnumerator IZoomOut(float totalTime, float targetSize, Vector3 targetPos, Action func = null)
        {
            float time = 0f;

            while (time <= totalTime)
            {
                m_cam.transform.position = Vector3.Lerp(m_nowCamPos, targetPos, (time / totalTime));
                m_cam.orthographicSize = Mathf.Lerp(m_nowCamSize, targetSize, (time / totalTime));

                time += Time.smoothDeltaTime;

                yield return null;
            }

            m_cam.orthographicSize = m_nowCamSize = targetSize;
            m_cam.transform.position = m_nowCamPos = targetPos;

            func?.Invoke();
        }

        public IEnumerator IZoomOut(float targetSize, Vector3 targetPos, Action func = null)
        {
            m_cam.orthographicSize = m_nowCamSize = targetSize;
            m_cam.transform.position = m_nowCamPos = targetPos;

            yield return null;

            func?.Invoke();
        }

        public IEnumerator Shake(float totalTime, float power, Action func = null)
        {
            power /= 10;

            float time = 0f;
            Vector3 originPos = new Vector3(0, 0, -10);
            GameObject backGround = Singleton.game.backGround;
            if(power >= 0.1f)
                backGround.transform.localScale = Vector3.one * 0.9f;

            while (time <= totalTime)
            {
                m_cam.transform.position = new Vector3(
                    originPos.x + UnityEngine.Random.insideUnitCircle.x * power,
                    originPos.y + UnityEngine.Random.insideUnitCircle.y * power,
                    -10);

                time += Time.smoothDeltaTime;
                yield return null;
            }

            backGround.transform.localScale = Vector3.one * 0.8f;
            m_cam.transform.position = new Vector3(0, 0, -10);

            func?.Invoke();
        }
    }
}