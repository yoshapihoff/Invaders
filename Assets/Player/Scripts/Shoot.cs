using UnityEngine;
using UnityEngine.UI;

namespace Invaders.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField]
        private Transform Muzzle;
        [SerializeField]
        private Weapon[] Weapon;
        [SerializeField]
        private Text WeaponText;

        private int WeaponId;

        private float LastFireTime = float.NegativeInfinity;

        private void Fire()
        {
            if (Input.GetButton("Fire1"))
            {
                if (Time.time > LastFireTime + Weapon[WeaponId].GetInterval())
                {
                    var shoot = Instantiate<GameObject>(Weapon[WeaponId].gameObject);
                    shoot.transform.position = Muzzle.position;

                    LastFireTime = Time.time;
                }
            }
        }

        private void SelectWeapon()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                ++WeaponId;
            }
            if (scroll < 0f)
            {
                --WeaponId;
            }
            if (WeaponId > Weapon.Length - 1)
            {
                WeaponId = 0;
            }
            if (WeaponId < 0)
            {
                WeaponId = Weapon.Length - 1;
            }
            UpdateWeaponText();
        }

        private void UpdateWeaponText()
        {
            WeaponText.text = string.Format("weapon: {0}", Weapon[WeaponId].GetName());
        }

        void Start()
        {
            if (Weapon.Length == 0)
            {
                Debug.Log("No weapon!");
                enabled = false;
            }
            WeaponId = 0;
            UpdateWeaponText();
        }

        void Update()
        {
            Fire();
            SelectWeapon();
        }
    }
}