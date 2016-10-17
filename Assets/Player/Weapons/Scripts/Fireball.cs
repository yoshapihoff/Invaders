using UnityEngine;
using Invaders.Invader;

namespace Invaders.Player
{
    public class Fireball : SingleBulletWeapon
    {
        [SerializeField]
        private int Damage = 3;
        [SerializeField]
        private ParticleSystem MoveEffect;
        [SerializeField]
        private ParticleSystem DestroyEffect;
        [SerializeField]
        private SpriteRenderer Renderer;
        [SerializeField]
        private Rigidbody2D RigidBody;
        [SerializeField]
        private float MoveSpeed = 5f;
        [SerializeField]
        private Collider2D Collider;

        protected override void CollideWithInvader(Controller invader)
        {
            invader.TakeDamage(Damage, invader.gameObject.GetInstanceID());

            Collider.enabled = false;

            MoveEffect.Stop();
            Renderer.enabled = false;

            DestroyEffect.Play();
            Destroy(gameObject, DestroyEffect.duration);
        }

        protected override void BeforeShoot()
        {
            MoveEffect.Play();
        }

        protected override void Move()
        {
            RigidBody.AddForce(Vector2.up * MoveSpeed);
        }

        new private void Start()
        {
            base.Start();
            Collider = GetComponent<Collider2D>();
        }
    }
}