using UnityEngine;

namespace Zero
{

    public class Quit : MonoBehaviour
    {
        public void Close()
        {
            Singleton.ui.CloseQuitPopUp();
        }

        public void Yes()
        {
            Application.Quit();
        }

    }

}