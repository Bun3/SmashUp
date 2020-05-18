using UnityEngine.EventSystems;
using UnityEngine;

namespace Zero
{

    public class SceneSelect : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Scene Scene = Scene.Main;

        public void OnPointerClick(PointerEventData eventData)
        {
            Singleton.scene.ChangeScene(Scene);
        }
    }

}