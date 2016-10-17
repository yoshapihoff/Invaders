using UnityEngine;
using System.Collections.Generic;

namespace Invaders
{
    public class InvadersController : MonoBehaviour
    {
        public enum InvaderType
        {
            None,
            Red,
            Orange,
            Yellow,
            Green,
            Cayn,
            Blue,
            Violet
        }

        public static InvadersController Instance;

        [SerializeField]
        private GameObject InvaderPrefab;
        [SerializeField]
        private int Rows = 20;

        private Transform ThisTransform;
        private List<GameObject> InvadersList;

        public static Color GetColorByInvaderType(InvaderType type)
        {
            switch ( type )
            {
                case InvaderType.Red:
                    return Color.red;

                case InvaderType.Orange:
                    return new Color(0.87f, 0.43f, 0.17f);

                case InvaderType.Yellow:
                    return Color.yellow;

                case InvaderType.Green:
                    return Color.green;

                case InvaderType.Cayn:
                    return Color.cyan;

                case InvaderType.Blue:
                    return Color.blue;

                case InvaderType.Violet:
                    return new Color(0.5f, 0.2f, 0.9f);

                default:
                    return Color.black;
            }
        }

        public static InvaderType GetRandomType ()
        {
            return (InvaderType)Random.Range(1, 8);
        }

        public void CreateInvaders ()
        {
            Camera cam = Camera.main;
            float halfWidth = cam.orthographicSize * cam.aspect;
            float halfHeight = cam.orthographicSize;
            Vector2 invaderArea = (Vector2)InvaderPrefab.GetComponentInChildren<Renderer>().bounds.size * 2f;
            float size = (invaderArea.x + invaderArea.y) / 2f;

            int cols = Mathf.RoundToInt((halfWidth * 2f) / size);

            for (int y = 0; y < Rows - 1; ++y )
            {
                for (int x = -(cols /2); x < (cols / 2); ++x )
                {
                    float posX = (x * halfWidth * 2f) / cols + (size / 2f);
                    float posY = y * size;
                    Vector2 pos = new Vector2(posX, posY);
                    var invader = Instantiate<GameObject>(InvaderPrefab);

                    invader.transform.SetParent(ThisTransform);
                    invader.transform.localPosition = pos;

                    InvadersList.Add(invader);
                }
            }

            for (int y = 1; y < Rows; ++y)
            {
                for (int x = -(cols / 2) + 1; x < (cols / 2); ++x)
                {
                    float posX = (x * halfWidth * 2f) / cols;
                    float posY = (y * size) - (size / 2f);
                    Vector2 pos = new Vector2(posX, posY);
                    var invader = Instantiate<GameObject>(InvaderPrefab);

                    invader.transform.SetParent(ThisTransform);
                    invader.transform.localPosition = pos;

                    InvadersList.Add(invader);
                }
            }
        }

        void Awake ()
        {
            Instance = this;
            InvadersList = new List<GameObject>();
        }

        void Start ()
        {
            ThisTransform = GetComponent<Transform>();
            CreateInvaders();
        }
    }
}