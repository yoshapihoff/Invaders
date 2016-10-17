using UnityEngine;
using System.Collections;

namespace Invaders.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float Speed;
        [SerializeField]
        private float PlayerHalfWidth;

        private Rigidbody2D ThisRB;
        private Camera Cam;
        private float HalfWidth;

        void Start ()
        {
            ThisRB = GetComponent<Rigidbody2D>();
            Cam = Camera.main;
            HalfWidth = Cam.orthographicSize * Cam.aspect;
        }

        void Update ()
        {
            var thisPosX = ThisRB.position.x + Input.GetAxis("Mouse X") * Time.deltaTime * Speed;
            thisPosX = Mathf.Clamp(thisPosX, -HalfWidth + PlayerHalfWidth, HalfWidth - PlayerHalfWidth);
            ThisRB.position = new Vector2(thisPosX, ThisRB.position.y);
        }
    }
}