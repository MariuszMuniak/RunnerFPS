using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.UI
{
    public class MouseActivator : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    } 
}
