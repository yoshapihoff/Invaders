using UnityEngine;
using UnityEngine.UI;

namespace Invaders.Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private int MaxHitPoints = 30;
        [SerializeField]
        private Text HealthText;
        [SerializeField]
        private Text GameOverText;
        [SerializeField]
        private ParticleSystem DamageEffect;

        private int HitPoints;

        public void TakeDamage(int amount)
        {
            HitPoints -= amount;
            DamageEffect.Play();

            if (HitPoints <= 0)
            {
                HitPoints = 0;
                GameOverText.enabled = true;
                Time.timeScale = 0f;
            }
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            HealthText.text = string.Format("health: {0}", HitPoints);
        }

        void Start()
        {
            HitPoints = MaxHitPoints;
            UpdateHealthText();
        }
    }
}