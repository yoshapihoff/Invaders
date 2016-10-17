using UnityEngine;
using System.Collections;

namespace Invaders.Player
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        private string Name;
        [SerializeField]
        private float Interval;
        [SerializeField]
        private float DestroyTime = 5f;
      
        protected abstract void BeforeShoot();
        
        public string GetName()
        {
            return Name;
        }

        public float GetInterval()
        {
            return Interval;
        }

        protected void Start()
        {
            BeforeShoot();
            Destroy(gameObject, DestroyTime);
        }
    }
}