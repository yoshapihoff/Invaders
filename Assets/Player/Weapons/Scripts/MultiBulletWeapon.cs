using UnityEngine;
using System.Collections.Generic;

namespace Invaders.Player
{
    public abstract class MultiBulletWeapon : Weapon
    {
        [SerializeField]
        protected GameObject BulletPrefab;
        [SerializeField]
        protected int BulletsCount;
        protected List<GameObject> Bullets = new List<GameObject>();

        public abstract void CollideWithInvader(Invader.Controller invader);
    }
}