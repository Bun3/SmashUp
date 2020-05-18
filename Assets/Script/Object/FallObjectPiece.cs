using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zero
{

    public class FallObjectPiece : MonoBehaviour
    {
        private PolygonCollider2D col;
        private Rigidbody2D body;
        private SpriteRenderer sprite;

        public PolygonCollider2D Col { get => col; set => col = value; }
        public Rigidbody2D Body { get => body; set => body = value; }
        public SpriteRenderer Sprite { get => sprite; set => sprite = value; }

        public void PieceInit()
        {
            sprite = GetComponent<SpriteRenderer>();
            col = GetComponent<PolygonCollider2D>();
            body = GetComponent<Rigidbody2D>();

            body.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Explosion()
        {
            col.enabled = true;
            body.AddForce(Random.insideUnitCircle * 10f, ForceMode2D.Impulse);
            StartCoroutine(IDestroyObject(2f));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Bottom") && transform.parent.gameObject != null)
            {
                StartCoroutine(IDestroyObject());
            }
        }

        public IEnumerator IDestroyObject(float waitTime = 1f)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(this.transform.parent.gameObject);
        }

        public IEnumerator ICrackingPiece()
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(-2, 3)), Space.Self);
            transform.position = transform.position + (Vector3)(Random.insideUnitCircle / 50);
            yield break;
        }

    }

}