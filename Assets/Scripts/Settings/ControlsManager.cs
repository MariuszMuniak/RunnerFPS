using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPS.Settings
{
    public class ControlsManager : MonoBehaviour
    {
        Settings settings;
        FirstPersonController firstPersonController;

        private void Awake()
        {
            settings = FindObjectOfType<Settings>();

            try
            {
                firstPersonController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
            }
            catch (System.Exception)
            {
                Debug.Log("Firest Person COntroller not found");
            }
        }

        void Start()
        {
            Sensitivity(settings.Sensitivity);
        }

        public void Sensitivity(float value)
        {
            settings.Sensitivity = value;

            if (firstPersonController == null) { return; }

            MouseLook mouseLook = firstPersonController.GetMouseLook();

            mouseLook.XSensitivity = value;
            mouseLook.YSensitivity = value;
        }

        public float GetSensitivityValue()
        {
            return settings.Sensitivity;
        }
    }
}
