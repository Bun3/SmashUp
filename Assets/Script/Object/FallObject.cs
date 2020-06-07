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

        private static InGame gameScene = null;

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

            if (gameScene == null) gameScene = GameObject.FindGameObjectWithTag("InGame").GetComponent<InGame>();

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
            StartCoroutine(ISetPositionAtTime(transform, new Vector2(0, targetPosY), 0.2f));

            Singleton.command.StartInputTime();
            StartCoroutine(gameScene.ITimeCheck());

            for (int i = 0; i < pieces.Length; i++)
            {
                //슬로우모션 연출
                pieces[i].Body.bodyType = RigidbodyType2D.Dynamic;
                pieces[i].Body.gravityScale = 0.01f;
            }
            yield break;
        }
        private IEnumerator ISetPositionAtTime(Transform ts, Vector2 targetPos, float endTime)
        {
            Vector2 startPos = ts.position;
            float timer = 0f;

            while (timer <= endTime)
            {
                ts.position = Vector2.Lerp(startPos, targetPos, timer / endTime);
                timer += Time.smoothDeltaTime;

                yield return null;
            }

            ts.position = targetPos;

            yield break;
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