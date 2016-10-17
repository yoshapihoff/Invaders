using UnityEngine;
using System.Collections;

namespace Invaders.Player
{
    public class ShotgunBullet : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D RigidBody;
        [SerializeField]
        private ParticleSystem DestroyEffect;
        [SerializeField]
        private SpriteRenderer Renderer;
        [SerializeField]
        private Collider2D Collider;

        private Transform ThisTransform;
        private int Damage;
        private float MoveSpeed;
        private Shotgun Gun;



        public void Setup(Shotgun gun, int damage, float moveSpeed)
        {
            Gun = gun;
            Damage = damage;
            MoveSpeed = moveSpeed;
        }

        void Start()
        {
            ThisTransform = GetComponent<Transform>();
        }

        void Update()
        {
            RigidBody.AddForce(ThisTransform.up * MoveSpeed);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Tags.INVADER))
            {
                var invader = other.gameObject.GetComponent<Invader.Controller>();
                Gun.CollideWithInvader(invader);

                Renderer.enabled = false;
                Collider.enabled = false;

                RigidBody.isKinematic = true;

                DestroyEffect.Play();
                Destroy(gameObject, DestroyEffect.duration);
            }
        }
    }
}