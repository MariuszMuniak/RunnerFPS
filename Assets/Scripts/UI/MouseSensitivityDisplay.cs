using FPS.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  FPS.UI
{
    public class MouseSensitivityDisplay : MonoBehaviour
    {
        [SerializeField] ControlsManager controlsManager;

        Text textValue;

        private void Awake()
        {
            textValue = GetComponent<Text>();
        }

        void Update()
        {
            textValue.text = string.Format("{0:00}", controlsManager.GetSensitivityValue());
        }
    } 
}
