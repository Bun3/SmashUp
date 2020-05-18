using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{

    public class FallObject : MonoBehaviour
    {
        private FallObjectPiece[] pieces;

        [SerializeField]
        private float startPosY = 0;
        [SerializeField]
        private float targetPosY = 0;

        private InGame gameScene;

        private void Awake()
        {
            transform.position = new Vector2(0, startPosY);

            pieces = new FallObjectPiece[transform.childCount];
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = transform.GetChild(i).GetComponent<FallObjectPiece>();
                pieces[i].PieceInit();
                pieces[i].Sprite.sortingOrder = 1;
            }

            gameScene = GameObject.FindGameObjectWithTag("InGame").GetComponent<InGame>();

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FallObject"), LayerMask.NameToLayer("FallObject"), true);
        }

        private void Start()
        {
            StartCoroutine(IDropObject());
        }

        public IEnumerator IExplosion()
        {
            Singleton.command.EndInputTime();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FallObject"), LayerMask.NameToLayer("FallObject"), false);

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].Explosion();
                pieces[i].Body.gravityScale = 1f;
            }
            yield return new WaitForSeconds(0.2f);

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FallObject"), LayerMask.NameToLayer("FallObject"), true);
        }

        private IEnumerator IDropObject()
        {
            float endTime = 0.2f;
            float timer = 0f;

            Vector2 startPos = transform.position;

            while (timer <= endTime)
            {
                transform.position = Vector2.Lerp(startPos, new Vector2(0, targetPosY), timer / endTime);
                timer += Time.smoothDeltaTime;

                yield return null;
            }

            transform.position = new Vector2(0, targetPosY);

            Singleton.command.StartInputTime();
            StartCoroutine(gameScene.ITimeCheck());

            for (int i = 0; i < pieces.Length; i++)
            {
                //슬로우모션 연출
                pieces[i].Body.bodyType = RigidbodyType2D.Dynamic;
                pieces[i].Body.gravityScale = 0.01f;
            }

        }

        public IEnumerator IFail()
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].Body.gravityScale = 50;
                pieces[i].Col.enabled = true;
                pieces[i].Sprite.sortingOrder = 5;
            }

            yield break;
        }

        public IEnumerator ICracking()
        {
            for (int i = 0; i < pieces.Length; i++)
                StartCoroutine(pieces[i].ICrackingPiece());
            yield break;
        }

    }

}