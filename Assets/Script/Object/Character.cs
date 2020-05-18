using System.Collections;
using UnityEngine;

namespace Zero
{

    public class Character : MonoBehaviour
    {
        CapsuleCollider2D capsule;
        Animator animator;
        SpriteRenderer sr;
        Rigidbody2D body;
        Transform ts;

        public Animator Animator { get => animator; set => animator = value; }
        public bool IsLookLeft { get => sr.flipX; set => sr.flipX = value; }

        private void Awake()
        {
            capsule = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
            ts = GetComponent<Transform>();
        }

        void Start()
        {
            SetCharacter(DataManager.GameData.CharIndex);
        }

        public void SetCharacter(int index)
        {
            animator.SetInteger("Index", index);
        }

    }

};