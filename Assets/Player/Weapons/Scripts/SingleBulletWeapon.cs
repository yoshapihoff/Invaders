using UnityEngine;
using System.Collections;

namespace Invaders.Player
{
    public abstract class SingleBulletWeapon : Weapon
    {
        protected abstract void Move();
        protected abstract void CollideWithInvader(Invader.Controller invader);

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Tags.INVADER))
            {
                var invader = other.gameObject.GetComponent<Invader.Controller>();
                CollideWithInvader(invader);
            }
        }

        protected void Update()
        {
            Move();
        }
    }
}
