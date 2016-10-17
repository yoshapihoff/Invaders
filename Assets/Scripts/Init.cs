using UnityEngine;
using System.Collections;

namespace Invaders
{
    public class Init : MonoBehaviour
    {
        void Start ()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}