using UnityEngine;
using Invaders.Invader;

namespace Invaders.Player
{
    public class Shotgun : MultiBulletWeapon
    {
        [SerializeField]
        private int Damage = 1;
        [SerializeField]
        private float MoveSpeed = 5f;
        [SerializeField]
        private float Angle;

        private Transform ThisTransform;

        protected override void BeforeShoot()
        {
            for(int i = 0; i < BulletsCount; ++i)
            {
                var bullet = Instantiate<GameObject>(BulletPrefab);
                bullet.GetComponent<ShotgunBullet>().Setup(this, Damage, MoveSpeed);
                var bulletTr = bullet.transform;
                bulletTr.SetParent(ThisTransform);
                bulletTr.localPosition = new Vector2((i - BulletsCount / 2) * 0.2f, 0f);
                float angle = Angle * (BulletsCount / 2 - i);
                bulletTr.Rotate(0f, 0f, angle, Space.World);

                Bullets.Clear();
                Bullets.Add(bullet);
            }
        }

        public override void CollideWithInvader(Controller invader)
        {
            invader.TakeDamage(Damage, invader.gameObject.GetInstanceID());
        }

        new private void Start()
        {
            ThisTransform = GetComponent<Transform>();

            base.Start();
        }
    }
}