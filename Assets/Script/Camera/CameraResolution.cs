using UnityEngine;
using UnityEngine.UI;

namespace Zero
{
    public class CameraResolution : MonoBehaviour
    {
        CanvasScaler uiCanvasScaler = null;
        Camera cam = null;

        private void Awake()
        {
            #region CameraRect

            cam = GetComponent<Camera>();
            Rect rect = cam.rect;
            float scaleH = ((float)Screen.width / Screen.height) / ((float)9 / 16);
            float scaleW = 1f / scaleH;
            if (scaleH < 1)
            {
                rect.height = scaleH;
                rect.y = (1f - scaleH) / 2f;
            }
            else
            {
                rect.width = scaleW;
                rect.x = (1f - scaleW) / 2f;
            }
            cam.rect = rect;

            #endregion

            #region CanvasMatch

            uiCanvasScaler = GameObject.Find("[UI]").GetComponent<CanvasScaler>();
            float ratio = (float)Screen.width / Screen.height;

            int match = 0;
            if (ratio <= 0.5)
                match = 0;
            else
                match = 1;

            uiCanvasScaler.matchWidthOrHeight = match;

            #endregion
        }

        private void OnPreCull()
        {
            //if (Application.isEditor) return;
            Rect camRect = cam.rect;
            Rect nr = new Rect(0, 0, 1, 1);

            cam.rect = nr;
            GL.Clear(true, true, Color.black);
            cam.rect = camRect;
        }

        private void OnPreRender()
        {
            //if (Application.isEditor) return;
            Rect camRect = cam.rect;
            Rect nr = new Rect(0, 0, 1, 1);

            cam.rect = nr;
            GL.Clear(true, true, Color.black);
            cam.rect = camRect;
        }

    }

}