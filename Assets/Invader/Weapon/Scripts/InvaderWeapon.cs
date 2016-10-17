using UnityEngine;
using System.Collections;

namespace Invaders.Invader
{
    public class InvaderWeapon : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D RigidBody;
        [SerializeField]
        private float Speed;
        [SerializeField]
        private float DestroyTime = 5f;
        [SerializeField]
        private int Damage = 1;
        private Transform ThisTransform;

        void Start ()
        {
            ThisTransform = GetComponent<Transform>();
            Destroy(gameObject, DestroyTime);
        }

        void Update()
        {
            RigidBody.AddForce(-(ThisTransform.right * Speed));
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PLAYER))
            {
                var playerHealthController = other.GetComponent<Player.Health>();
                playerHealthController.TakeDamage(Damage);
            }
        }
    }
}