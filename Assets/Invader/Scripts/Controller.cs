using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Invaders.Invader
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private InvadersController.InvaderType Type;
        [SerializeField]
        private int MaxHitPoints = 12;
        [SerializeField]
        private Collider2D WaveDamageCollider;
        [SerializeField]
        private SpriteRenderer Renderer;
        [SerializeField]
        private ParticleSystem WaveDamageEffect;
        [SerializeField]
        private ParticleSystem DestroyEffect;
        [SerializeField]
        private Animator DamageAnimator;
        [SerializeField]
        private Text DamageText;
        [SerializeField]
        private float Speed = 0.02f;
        [SerializeField]
        private int CollideWithPlayerDamage = 4;

        [SerializeField]
        private GameObject ShootPrefab;
        [SerializeField]
        private float ShootInterval = 5f;
        [SerializeField]
        [Range(0, 1)]
        private float ShootPercent = 0.5f;
        [SerializeField]
        private float ShootYDistance = 7f;

        private Transform ThisTransform;
        private int HitPoints;
        private Transform PlayerTr;
        private float LastShootTime = 0f;
        private Text GameOverText;

        private int DamageAnimHash = Animator.StringToHash("Damage");

        private List<Controller> Neighbors = new List<Controller>();

        private void SetRandomType()
        {
            Type = InvadersController.GetRandomType();
        }

        private void SetColorByType()
        {
            Color color = InvadersController.GetColorByInvaderType(Type);
            Renderer.color = color;
            DestroyEffect.startColor = color;
            WaveDamageEffect.startColor = color;
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(ShootInterval * Random.value);

            var shoot = Instantiate<GameObject>(ShootPrefab);
            shoot.transform.position = ThisTransform.position;
            Vector2 subVector = shoot.transform.position - PlayerTr.position;
            float angle = Mathf.Atan2(subVector.y, subVector.x) * Mathf.Rad2Deg;
            shoot.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            shoot.GetComponentInChildren<SpriteRenderer>().color = InvadersController.GetColorByInvaderType(Type);
            shoot.GetComponentInChildren<TrailRenderer>().material.color = InvadersController.GetColorByInvaderType(Type);
        }

        public void TakeDamage(int amount, int invokerInstanceId, bool waveDamage = false)
        {
            if (amount <= 0)
            {
                return;
            }

            DamageText.text = amount.ToString();
            DamageAnimator.SetTrigger(DamageAnimHash);

            if (waveDamage && HitPoints - amount > 0)
            {
                WaveDamageEffect.Play();
            }

            HitPoints -= amount;

            for (int i = 0; i < Neighbors.Count; ++i)
            {
                var neighborController = Neighbors[i];
                if (neighborController)
                {
                    if (neighborController.gameObject.GetInstanceID() != invokerInstanceId)
                    {
                        neighborController.TakeDamage(--amount, gameObject.GetInstanceID(), true);
                    }
                }
            }

            if (HitPoints <= 0)
            {
                Renderer.enabled = false;
                DestroyEffect.Play();
                Destroy(gameObject, DestroyEffect.duration);
            }
        }

        void Awake ()
        {
            SetRandomType();
            SetColorByType();

            HitPoints = MaxHitPoints;

            ThisTransform = GetComponent<Transform>();
        }

        void Start()
        {
            PlayerTr = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Transform>();
            GameOverText = GameObject.FindGameObjectWithTag(Tags.GAMEOVER_TEXT).GetComponent<Text>();
        }

        void Update ()
        {
            ThisTransform.Translate(Vector2.down * Speed * Time.deltaTime);

            if (Mathf.Abs(ThisTransform.position.y - PlayerTr.position.y) < ShootYDistance)
            {
                if (Time.time > LastShootTime + ShootInterval)
                {
                    if (Random.value < ShootPercent)
                    {
                        StartCoroutine(Shoot());
                        LastShootTime = Time.time;
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.INVADER))
            {
                var otherController = other.GetComponent<Controller>();
                if (otherController.Type == Type)
                {
                    if (!Neighbors.Contains (otherController))
                    {
                        Neighbors.Add(otherController);
                    }
                }
            }
            if (other.CompareTag(Tags.GAMEOVER))
            {
                GameOverText.enabled = true;
                Time.timeScale = 0f;
            }
            if (other.CompareTag(Tags.PLAYER))
            {
                var playerHealthcontroller = other.GetComponent<Player.Health>();
                playerHealthcontroller.TakeDamage(CollideWithPlayerDamage);
            }
        }
    }
}